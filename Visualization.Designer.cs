namespace WF {
    partial class Visualization {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.button_start = new System.Windows.Forms.Button();
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.MainLabel = new System.Windows.Forms.Label();
            this.BeginButton = new System.Windows.Forms.Button();
            this.ChooseLabel = new System.Windows.Forms.Label();
            this.Real = new System.Windows.Forms.Button();
            this.Whole = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.EndButton = new System.Windows.Forms.Button();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.CountOfMarkersTextBox = new System.Windows.Forms.TextBox();
            this.CountOfMarkersLabel = new System.Windows.Forms.Label();
            this.GraphData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(152, 97);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(149, 74);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Старт\r\n";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.Start_button);
            // 
            // zedGraph
            // 
            this.zedGraph.Location = new System.Drawing.Point(0, 0);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(150, 150);
            this.zedGraph.TabIndex = 0;
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Location = new System.Drawing.Point(164, 58);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(115, 13);
            this.MainLabel.TabIndex = 1;
            this.MainLabel.Text = "Input the data of graph";
            // 
            // BeginButton
            // 
            this.BeginButton.Location = new System.Drawing.Point(188, 243);
            this.BeginButton.Name = "BeginButton";
            this.BeginButton.Size = new System.Drawing.Size(75, 23);
            this.BeginButton.TabIndex = 3;
            this.BeginButton.Text = "OK";
            this.BeginButton.UseVisualStyleBackColor = true;
            this.BeginButton.Click += new System.EventHandler(this.BeginButton_Click);
            // 
            // ChooseLabel
            // 
            this.ChooseLabel.AutoSize = true;
            this.ChooseLabel.Location = new System.Drawing.Point(185, 81);
            this.ChooseLabel.Name = "ChooseLabel";
            this.ChooseLabel.Size = new System.Drawing.Size(75, 13);
            this.ChooseLabel.TabIndex = 4;
            this.ChooseLabel.Text = "Real or Whole";
            // 
            // Real
            // 
            this.Real.Location = new System.Drawing.Point(139, 123);
            this.Real.Name = "Real";
            this.Real.Size = new System.Drawing.Size(75, 23);
            this.Real.TabIndex = 5;
            this.Real.Text = "Real";
            this.Real.UseVisualStyleBackColor = true;
            this.Real.Click += new System.EventHandler(this.Real_Click);
            // 
            // Whole
            // 
            this.Whole.Location = new System.Drawing.Point(242, 123);
            this.Whole.Name = "Whole";
            this.Whole.Size = new System.Drawing.Size(75, 23);
            this.Whole.TabIndex = 6;
            this.Whole.Text = "Whole";
            this.Whole.UseVisualStyleBackColor = true;
            this.Whole.Click += new System.EventHandler(this.Whole_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(34, 149);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(127, 13);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "Input time of the research";
            // 
            // EndButton
            // 
            this.EndButton.Location = new System.Drawing.Point(188, 226);
            this.EndButton.Name = "EndButton";
            this.EndButton.Size = new System.Drawing.Size(75, 23);
            this.EndButton.TabIndex = 8;
            this.EndButton.Text = "OK";
            this.EndButton.UseVisualStyleBackColor = true;
            this.EndButton.Click += new System.EventHandler(this.EndButton_Click);
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Location = new System.Drawing.Point(48, 172);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeTextBox.TabIndex = 9;
            // 
            // CountOfMarkersTextBox
            // 
            this.CountOfMarkersTextBox.Location = new System.Drawing.Point(320, 172);
            this.CountOfMarkersTextBox.Name = "CountOfMarkersTextBox";
            this.CountOfMarkersTextBox.Size = new System.Drawing.Size(100, 20);
            this.CountOfMarkersTextBox.TabIndex = 10;
            // 
            // CountOfMarkersLabel
            // 
            this.CountOfMarkersLabel.AutoSize = true;
            this.CountOfMarkersLabel.Location = new System.Drawing.Point(293, 149);
            this.CountOfMarkersLabel.Name = "CountOfMarkersLabel";
            this.CountOfMarkersLabel.Size = new System.Drawing.Size(143, 13);
            this.CountOfMarkersLabel.TabIndex = 11;
            this.CountOfMarkersLabel.Text = "Input count of markers for go";
            // 
            // GraphData
            // 
            this.GraphData.Location = new System.Drawing.Point(179, 97);
            this.GraphData.Multiline = true;
            this.GraphData.Name = "GraphData";
            this.GraphData.Size = new System.Drawing.Size(100, 107);
            this.GraphData.TabIndex = 12;
            // 
            // Visualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 288);
            this.Controls.Add(this.GraphData);
            this.Controls.Add(this.CountOfMarkersLabel);
            this.Controls.Add(this.CountOfMarkersTextBox);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.EndButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.Whole);
            this.Controls.Add(this.Real);
            this.Controls.Add(this.ChooseLabel);
            this.Controls.Add(this.BeginButton);
            this.Controls.Add(this.MainLabel);
            this.Controls.Add(this.button_start);
            this.Name = "Visualization";
            this.Text = "Graph Research";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.Button BeginButton;
        private System.Windows.Forms.Label ChooseLabel;
        private System.Windows.Forms.Button Real;
        private System.Windows.Forms.Button Whole;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button EndButton;
        private System.Windows.Forms.TextBox TimeTextBox;
        private System.Windows.Forms.TextBox CountOfMarkersTextBox;
        private System.Windows.Forms.Label CountOfMarkersLabel;
        private System.Windows.Forms.TextBox GraphData;
    }
}

