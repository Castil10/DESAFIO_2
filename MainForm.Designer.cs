using System.Windows.Forms;

namespace TechopolisTransport
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cboOrigen = new ComboBox();
            cboDestino = new ComboBox();
            lblOrigen = new Label();
            lblDestino = new Label();
            btnBfs = new Button();
            btnDfs = new Button();
            btnRutaCorta = new Button();
            dgvRutas = new DataGridView();
            lstResultado = new ListBox();
            graphBoard = new GraphCanvas();
            lblRutas = new Label();
            lblResultado = new Label();
            lblMapa = new Label();
            cboMapa = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvRutas).BeginInit();
            SuspendLayout();
            // 
            // cboOrigen
            // 
            cboOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOrigen.FormattingEnabled = true;
            cboOrigen.Location = new Point(27, 42);
            cboOrigen.Name = "cboOrigen";
            cboOrigen.Size = new Size(200, 28);
            cboOrigen.TabIndex = 0;
            // 
            // cboDestino
            // 
            cboDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDestino.FormattingEnabled = true;
            cboDestino.Location = new Point(255, 42);
            cboDestino.Name = "cboDestino";
            cboDestino.Size = new Size(200, 28);
            cboDestino.TabIndex = 1;
            // 
            // lblOrigen
            // 
            lblOrigen.AutoSize = true;
            lblOrigen.Location = new Point(24, 18);
            lblOrigen.Name = "lblOrigen";
            lblOrigen.Size = new Size(54, 20);
            lblOrigen.TabIndex = 7;
            lblOrigen.Text = "Origen";
            // 
            // lblDestino
            // 
            lblDestino.AutoSize = true;
            lblDestino.Location = new Point(252, 18);
            lblDestino.Name = "lblDestino";
            lblDestino.Size = new Size(60, 20);
            lblDestino.TabIndex = 8;
            lblDestino.Text = "Destino";
            // 
            // btnBfs
            // 
            btnBfs.Location = new Point(475, 27);
            btnBfs.Name = "btnBfs";
            btnBfs.Size = new Size(112, 29);
            btnBfs.TabIndex = 2;
            btnBfs.Text = "Recorrido BFS";
            btnBfs.UseVisualStyleBackColor = true;
            btnBfs.Click += btnBfs_Click;
            // 
            // btnDfs
            // 
            btnDfs.Location = new Point(593, 27);
            btnDfs.Name = "btnDfs";
            btnDfs.Size = new Size(112, 29);
            btnDfs.TabIndex = 3;
            btnDfs.Text = "Recorrido DFS";
            btnDfs.UseVisualStyleBackColor = true;
            btnDfs.Click += btnDfs_Click;
            // 
            // btnRutaCorta
            // 
            btnRutaCorta.Location = new Point(475, 62);
            btnRutaCorta.Name = "btnRutaCorta";
            btnRutaCorta.Size = new Size(230, 29);
            btnRutaCorta.TabIndex = 4;
            btnRutaCorta.Text = "Ruta más corta (Dijkstra)";
            btnRutaCorta.UseVisualStyleBackColor = true;
            btnRutaCorta.Click += btnRutaCorta_Click;
            // 
            // dgvRutas
            // 
            dgvRutas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRutas.Location = new Point(24, 155);
            dgvRutas.Name = "dgvRutas";
            dgvRutas.RowHeadersWidth = 51;
            dgvRutas.Size = new Size(386, 159);
            dgvRutas.TabIndex = 6;
            // 
            // lstResultado
            // 
            lstResultado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstResultado.FormattingEnabled = true;
            lstResultado.Location = new Point(24, 343);
            lstResultado.Name = "lstResultado";
            lstResultado.Size = new Size(386, 164);
            lstResultado.TabIndex = 7;
            // 
            // graphBoard
            // 
            graphBoard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            graphBoard.BackColor = Color.WhiteSmoke;
            graphBoard.Location = new Point(416, 155);
            graphBoard.Name = "graphBoard";
            graphBoard.Size = new Size(316, 381);
            graphBoard.TabIndex = 9;
            // 
            // lblRutas
            // 
            lblRutas.AutoSize = true;
            lblRutas.Location = new Point(24, 132);
            lblRutas.Name = "lblRutas";
            lblRutas.Size = new Size(153, 20);
            lblRutas.TabIndex = 10;
            lblRutas.Text = "Rutas y tiempos (min)";
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(24, 320);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(75, 20);
            lblResultado.TabIndex = 11;
            lblResultado.Text = "Resultado";
            // 
            // lblMapa
            // 
            lblMapa.AutoSize = true;
            lblMapa.Location = new Point(24, 78);
            lblMapa.Name = "lblMapa";
            lblMapa.Size = new Size(134, 20);
            lblMapa.TabIndex = 12;
            lblMapa.Text = "Mapa pre-cargado";
            // 
            // cboMapa
            // 
            cboMapa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMapa.FormattingEnabled = true;
            cboMapa.Location = new Point(27, 101);
            cboMapa.Name = "cboMapa";
            cboMapa.Size = new Size(307, 28);
            cboMapa.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 531);
            Controls.Add(cboMapa);
            Controls.Add(lblMapa);
            Controls.Add(lblResultado);
            Controls.Add(lblRutas);
            Controls.Add(graphBoard);
            Controls.Add(lstResultado);
            Controls.Add(dgvRutas);
            Controls.Add(btnRutaCorta);
            Controls.Add(btnDfs);
            Controls.Add(btnBfs);
            Controls.Add(lblDestino);
            Controls.Add(lblOrigen);
            Controls.Add(cboDestino);
            Controls.Add(cboOrigen);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Techopolis Transport";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRutas).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private ComboBox cboOrigen;
        private ComboBox cboDestino;
        private Label lblOrigen;
        private Label lblDestino;
        private Button btnBfs;
        private Button btnDfs;
        private Button btnRutaCorta;
        private DataGridView dgvRutas;
        private ListBox lstResultado;
        private GraphCanvas graphBoard;
        private Label lblRutas;
        private Label lblResultado;
        private Label lblMapa;
        private ComboBox cboMapa;
    }
}
