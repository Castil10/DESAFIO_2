using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TechopolisTransport.Domain;

namespace TechopolisTransport
{
    public partial class MainForm : Form
    {
        private readonly Graph _graph = new();
        private readonly BindingList<MapDefinition> _maps = new();
        private BindingList<RouteViewModel> _routesBinding = new();

        public MainForm()
        {
            InitializeComponent();
            CrearMapasPredefinidos();
            InicializarGrafo();
            InicializarControles();
        }

        // ------------------ Cargar estaciones y rutas ------------------
        private void CrearMapasPredefinidos()
        {
            var mapaPrincipal = new MapDefinition(
                "Mapa urbano principal",
                new[]
                {
                    new Station("CEN", "Central"),
                    new Station("NOR", "Norte"),
                    new Station("SUR", "Sur"),
                    new Station("EST", "Este"),
                    new Station("OES", "Oeste"),
                    new Station("AER", "Aeropuerto"),
                    new Station("UNI", "Universidad")
                },
                new[]
                {
                    new MapRoute("CEN", "NOR", 10),
                    new MapRoute("CEN", "SUR", 12),
                    new MapRoute("CEN", "EST", 8),
                    new MapRoute("CEN", "OES", 9),
                    new MapRoute("NOR", "AER", 15),
                    new MapRoute("SUR", "UNI", 7),
                    new MapRoute("EST", "UNI", 10),
                    new MapRoute("OES", "AER", 18),
                    new MapRoute("UNI", "AER", 14)
                });

            var mapaAlternativo = new MapDefinition(
                "Corredor metropolitano",
                new[]
                {
                    new Station("TEC", "Terminal"),
                    new Station("IND", "Industrial"),
                    new Station("PAR", "Parque"),
                    new Station("MUS", "Museo"),
                    new Station("EST", "Estadio"),
                    new Station("BIS", "Bicentenario")
                },
                new[]
                {
                    new MapRoute("TEC", "IND", 6),
                    new MapRoute("IND", "PAR", 5),
                    new MapRoute("PAR", "MUS", 4),
                    new MapRoute("MUS", "EST", 7),
                    new MapRoute("EST", "BIS", 6),
                    new MapRoute("BIS", "TEC", 9),
                    new MapRoute("PAR", "EST", 5)
                });

            _maps.Add(mapaPrincipal);
            _maps.Add(mapaAlternativo);
        }

        private void InicializarGrafo()
        {
            if (_maps.Any())
            {
                _maps.First().Apply(_graph);
            }
        }

        // ------------------ Inicializar UI ------------------
        private void InicializarControles()
        {
            cboMapa.DataSource = _maps;
            cboMapa.DisplayMember = nameof(MapDefinition.Name);
            cboMapa.SelectedIndexChanged += (_, _) => CambiarMapaSeleccionado();

            cboOrigen.SelectedIndexChanged += (_, _) => ResaltarSeleccion();
            cboDestino.SelectedIndexChanged += (_, _) => ResaltarSeleccion();

            dgvRutas.AutoGenerateColumns = false;
            if (dgvRutas.Columns.Count == 0)
            {
                dgvRutas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(RouteViewModel.FromName),
                    HeaderText = "Desde",
                    ReadOnly = true
                });

                dgvRutas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(RouteViewModel.ToName),
                    HeaderText = "Hasta",
                    ReadOnly = true
                });

                dgvRutas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = nameof(RouteViewModel.TimeMinutes),
                    HeaderText = "Tiempo (min)"
                });
            }

            dgvRutas.CellEndEdit += DgvRutas_CellEndEdit;
            dgvRutas.CellValidating += DgvRutas_CellValidating;

            RecargarDatosDeMapa();
        }

        // ------------------ Validación de entradas ------------------
        private void DgvRutas_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var column = dgvRutas.Columns[e.ColumnIndex];
            if (!string.Equals(column.DataPropertyName, nameof(RouteViewModel.TimeMinutes), StringComparison.Ordinal))
                return;

            if (!int.TryParse(e.FormattedValue?.ToString(), out var minutes) || minutes <= 0)
            {
                e.Cancel = true;
                MessageBox.Show("Ingrese un tiempo válido en minutos (mayor a cero).",
                    "Valor inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ------------------ Actualizar tiempos desde la grilla ------------------
        private void DgvRutas_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = _routesBinding[e.RowIndex];

            if (item.TimeMinutes <= 0)
            {
                MessageBox.Show("El tiempo debe ser mayor a cero.",
                    "Valor inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                item.TimeMinutes = 1;
                dgvRutas.Refresh();
                return;
            }

            var from = _graph.FindStationByName(item.FromName);
            var to = _graph.FindStationByName(item.ToName);

            if (from == null || to == null) return;

            _graph.AddRoute(from, to, item.TimeMinutes); // actualiza peso en el grafo
            graphBoard.ApplyHeapLayout(ValorEstacion);
        }

        private void CambiarMapaSeleccionado()
        {
            if (cboMapa.SelectedItem is not MapDefinition map) return;

            map.Apply(_graph);
            RecargarDatosDeMapa();
        }

        private void RecargarDatosDeMapa()
        {
            var stationsList = _graph.Stations.ToList();

            cboOrigen.DataSource = stationsList;
            cboDestino.DataSource = stationsList.ToList();

            _routesBinding = new BindingList<RouteViewModel>(
                _graph.GetRoutes()
                      .Select(r => new RouteViewModel
                      {
                          FromId = r.From.Id,
                          ToId = r.To.Id,
                          FromName = r.From.Name,
                          ToName = r.To.Name,
                          TimeMinutes = r.TimeMinutes
                      }).ToList());

            dgvRutas.DataSource = _routesBinding;

            graphBoard.LoadGraph(_graph);
            graphBoard.ApplyHeapLayout(ValorEstacion);
            ResaltarSeleccion();
        }

        // ------------------ Helpers para obtener origen/destino ------------------
        private Station? GetSelectedOrigin()
        {
            if (cboOrigen.SelectedItem is Station s) return s;

            MessageBox.Show("Seleccione una estación de origen.",
                "Dato requerido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }

        private Station? GetSelectedDestination()
        {
            if (cboDestino.SelectedItem is Station s) return s;

            MessageBox.Show("Seleccione una estación de destino.",
                "Dato requerido",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return null;
        }

        // ------------------ Evento: BFS ------------------
        private void btnBfs_Click(object sender, EventArgs e)
        {
            lstResultado.Items.Clear();

            var origin = GetSelectedOrigin();
            if (origin == null) return;

            var recorrido = _graph.Bfs(origin);

            graphBoard.HighlightStations(recorrido);

            lstResultado.Items.Add($"Recorrido BFS desde {origin.Name}:");
            foreach (var station in recorrido)
            {
                lstResultado.Items.Add($"- {station.Name}");
            }
        }

        // ------------------ Evento: DFS ------------------
        private void btnDfs_Click(object sender, EventArgs e)
        {
            lstResultado.Items.Clear();

            var origin = GetSelectedOrigin();
            if (origin == null) return;

            var recorrido = _graph.Dfs(origin);

            graphBoard.HighlightStations(recorrido);

            lstResultado.Items.Add($"Recorrido DFS desde {origin.Name}:");
            foreach (var station in recorrido)
            {
                lstResultado.Items.Add($"- {station.Name}");
            }
        }

        // ------------------ Evento: Ruta más corta (Dijkstra) ------------------
        private void btnRutaCorta_Click(object sender, EventArgs e)
        {
            lstResultado.Items.Clear();

            var origin = GetSelectedOrigin();
            var dest = GetSelectedDestination();
            if (origin == null || dest == null) return;

            if (origin.Equals(dest))
            {
                MessageBox.Show("El origen y el destino deben ser diferentes.",
                    "Datos inválidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = _graph.ShortestPath(origin, dest);

            if (!result.HasPath)
            {
                lstResultado.Items.Add("No existe ruta entre las estaciones seleccionadas.");
                graphBoard.ClearHighlights();
                return;
            }

            graphBoard.HighlightStations(result.Path);

            lstResultado.Items.Add($"Ruta más corta de {origin.Name} a {dest.Name}:");
            for (int i = 0; i < result.Path.Count; i++)
            {
                var station = result.Path[i];
                if (i == result.Path.Count - 1)
                    lstResultado.Items.Add($"  {station.Name}");
                else
                    lstResultado.Items.Add($"  {station.Name} ->");
            }

            lstResultado.Items.Add($"Tiempo total: {result.TotalTime} minutos");
        }

        private void ResaltarSeleccion()
        {
            var seleccionados = new[] { cboOrigen.SelectedItem, cboDestino.SelectedItem }
                .OfType<Station>();
            graphBoard.HighlightStations(seleccionados);
        }

        private double ValorEstacion(Station station)
        {
            var tiempos = _graph.GetRoutes()
                .Where(r => r.From.Equals(station) || r.To.Equals(station))
                .Select(r => r.TimeMinutes)
                .ToList();

            if (tiempos.Count == 0)
                return 0;

            return 1d / tiempos.Average(); // menor tiempo => mayor prioridad en el montículo
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
