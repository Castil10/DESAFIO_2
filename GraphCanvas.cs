using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TechopolisTransport.Domain;

namespace TechopolisTransport
{
    public class GraphCanvas : Panel
    {
        private const int NodeRadius = 22;
        private readonly Dictionary<Station, PointF> _positions = new();
        private Graph? _graph;
        private bool _isDragging;
        private Station? _draggedStation;
        private PointF _dragOffset;
        private Func<Station, double>? _valueSelector;
        private HashSet<Station> _highlightedStations = new();
        private List<(Station From, Station To)> _highlightedSegments = new();

        public GraphCanvas()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.WhiteSmoke;
        }

        public void LoadGraph(Graph graph)
        {
            _graph = graph;
            _positions.Clear();
            ClearHighlights();
            EnsurePositions();
            Invalidate();
        }

        public void ApplyHeapLayout(Func<Station, double> valueSelector)
        {
            _valueSelector = valueSelector;
            if (_graph == null) return;

            var ordered = _graph.Stations
                .OrderByDescending(valueSelector)
                .ToList();

            if (ordered.Count == 0) return;

            var levels = (int)Math.Floor(Math.Log2(ordered.Count)) + 1;
            var levelHeight = Math.Max(70, Height / Math.Max(1, levels));

            for (int i = 0; i < ordered.Count; i++)
            {
                var level = (int)Math.Floor(Math.Log2(i + 1));
                var positionInLevel = i - ((1 << level) - 1);
                var maxNodes = 1 << level;

                var spacing = Width / (maxNodes + 1f);
                var x = spacing * (positionInLevel + 1);
                var y = (level + 0.5f) * levelHeight;

                _positions[ordered[i]] = ClampToCanvas(new PointF(x, y));
            }

            Invalidate();
        }

        public void RefreshLayout()
        {
            if (_valueSelector != null && _graph != null)
            {
                ApplyHeapLayout(_valueSelector);
            }
            else
            {
                EnsurePositions();
                Invalidate();
            }
        }

        public void HighlightStations(IEnumerable<Station> stations)
        {
            var ordered = stations.ToList();
            _highlightedStations = new HashSet<Station>(ordered);

            _highlightedSegments = new List<(Station From, Station To)>();
            for (int i = 0; i < ordered.Count - 1; i++)
            {
                var from = ordered[i];
                var to = ordered[i + 1];
                if (AreConnected(from, to))
                {
                    _highlightedSegments.Add((from, to));
                }
            }

            Invalidate();
        }

        public void ClearHighlights()
        {
            _highlightedStations.Clear();
            _highlightedSegments.Clear();
            Invalidate();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RefreshLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_graph == null || !_graph.Stations.Any())
                return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawEdges(g);
            DrawNodes(g);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_graph == null) return;

            var station = HitTest(e.Location);
            if (station != null)
            {
                _isDragging = true;
                _draggedStation = station;
                var center = _positions[station];
                _dragOffset = new PointF(e.Location.X - center.X, e.Location.Y - center.Y);
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_isDragging || _draggedStation == null) return;

            var clamped = ClampToCanvas(new PointF(e.Location.X - _dragOffset.X, e.Location.Y - _dragOffset.Y));
            _positions[_draggedStation] = clamped;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!_isDragging) return;

            _isDragging = false;
            _draggedStation = null;
            Cursor = Cursors.Default;
        }

        private void DrawEdges(Graphics g)
        {
            using var pen = new Pen(Color.Gray, 2)
            {
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                StartCap = System.Drawing.Drawing2D.LineCap.Round
            };

            using var highlightPen = new Pen(Color.OrangeRed, 3)
            {
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                StartCap = System.Drawing.Drawing2D.LineCap.Round
            };

            using var textBrush = new SolidBrush(Color.Black);
            using var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);

            foreach (var route in _graph!.GetRoutes())
            {
                var from = _positions[route.From];
                var to = _positions[route.To];

                var isHighlighted = _highlightedSegments.Any(seg =>
                    (seg.From.Equals(route.From) && seg.To.Equals(route.To)) ||
                    (seg.From.Equals(route.To) && seg.To.Equals(route.From)));

                g.DrawLine(isHighlighted ? highlightPen : pen, from, to);

                var midpoint = new PointF((from.X + to.X) / 2f, (from.Y + to.Y) / 2f);
                var label = $"{route.TimeMinutes} min";
                var size = g.MeasureString(label, font);
                g.FillRectangle(Brushes.White, midpoint.X - size.Width / 2, midpoint.Y - size.Height / 2, size.Width, size.Height);
                g.DrawString(label, font, textBrush, midpoint.X - size.Width / 2, midpoint.Y - size.Height / 2);
            }
        }

        private void DrawNodes(Graphics g)
        {
            using var fill = new SolidBrush(Color.FromArgb(80, 134, 247));
            using var stroke = new Pen(Color.MidnightBlue, 2);
            using var font = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.White);
            using var highlightFill = new SolidBrush(Color.FromArgb(255, 193, 59));
            using var highlightStroke = new Pen(Color.DarkOrange, 3);

            foreach (var station in _graph!.Stations)
            {
                var center = _positions[station];
                var rect = new RectangleF(center.X - NodeRadius, center.Y - NodeRadius, NodeRadius * 2, NodeRadius * 2);
                if (_highlightedStations.Contains(station))
                {
                    g.FillEllipse(highlightFill, rect);
                    g.DrawEllipse(highlightStroke, rect);
                }
                else
                {
                    g.FillEllipse(fill, rect);
                    g.DrawEllipse(stroke, rect);
                }

                var label = station.Name;
                var size = g.MeasureString(label, font);
                g.DrawString(label, font, textBrush, center.X - size.Width / 2, center.Y - size.Height / 2);
            }
        }

        private void EnsurePositions()
        {
            if (_graph == null) return;

            var stations = _graph.Stations.ToList();
            var missingStations = stations.Where(s => !_positions.ContainsKey(s)).ToList();
            if (missingStations.Count == 0 && _positions.Keys.Count == stations.Count)
                return;

            var radius = Math.Min(Width, Height) / 2f - NodeRadius - 10;
            if (radius <= 0) radius = 60;

            var center = new PointF(Width / 2f, Height / 2f);
            for (int i = 0; i < stations.Count; i++)
            {
                var angle = 2 * Math.PI * i / stations.Count;
                var x = center.X + (float)(radius * Math.Cos(angle));
                var y = center.Y + (float)(radius * Math.Sin(angle));
                _positions[stations[i]] = new PointF(x, y);
            }
        }

        private Station? HitTest(Point location)
        {
            foreach (var kvp in _positions)
            {
                var center = kvp.Value;
                var dx = location.X - center.X;
                var dy = location.Y - center.Y;
                if (Math.Sqrt(dx * dx + dy * dy) <= NodeRadius)
                    return kvp.Key;
            }

            return null;
        }

        private PointF ClampToCanvas(PointF point)
        {
            var x = Math.Max(NodeRadius, Math.Min(Width - NodeRadius, point.X));
            var y = Math.Max(NodeRadius, Math.Min(Height - NodeRadius, point.Y));
            return new PointF(x, y);
        }

        private bool AreConnected(Station from, Station to)
        {
            if (_graph == null) return false;
            return _graph.GetRoutes().Any(r =>
                (r.From.Equals(from) && r.To.Equals(to)) ||
                (r.From.Equals(to) && r.To.Equals(from)));
        }
    }
}
