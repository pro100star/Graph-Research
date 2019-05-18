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
            this._zedGraph_ = new ZedGraph.ZedGraphControl();
            this.DataListBox = new System.Windows.Forms.ListBox();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.ReadyButton = new System.Windows.Forms.Button();
            this.graphTypeButton = new System.Windows.Forms.Button();
            this.textTypeButton = new System.Windows.Forms.Button();
            this.selectTypeLabel = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadFromFile = new System.Windows.Forms.Button();
            this.saveGraphic = new System.Windows.Forms.Button();
            this.OneMarkerButton = new System.Windows.Forms.Button();
            this.someMarkersButton = new System.Windows.Forms.Button();
            this.SelectMarkersLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
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
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Location = new System.Drawing.Point(164, 58);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(118, 13);
            this.MainLabel.TabIndex = 1;
            this.MainLabel.Text = " Input the data of graph";
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
            this.ChooseLabel.Size = new System.Drawing.Size(167, 13);
            this.ChooseLabel.TabIndex = 4;
            this.ChooseLabel.Text = "Какой тип имеют ребра графа?";
            // 
            // Real
            // 
            this.Real.Location = new System.Drawing.Point(139, 123);
            this.Real.Name = "Real";
            this.Real.Size = new System.Drawing.Size(75, 39);
            this.Real.TabIndex = 5;
            this.Real.Text = "Вещественный";
            this.Real.UseVisualStyleBackColor = true;
            this.Real.Click += new System.EventHandler(this.Real_Click);
            // 
            // Whole
            // 
            this.Whole.Location = new System.Drawing.Point(242, 123);
            this.Whole.Name = "Whole";
            this.Whole.Size = new System.Drawing.Size(75, 23);
            this.Whole.TabIndex = 6;
            this.Whole.Text = "Целый";
            this.Whole.UseVisualStyleBackColor = true;
            this.Whole.Click += new System.EventHandler(this.Whole_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(34, 149);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(159, 13);
            this.TimeLabel.TabIndex = 7;
            this.TimeLabel.Text = "Введите время исследования";
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
            this.CountOfMarkersLabel.Size = new System.Drawing.Size(358, 13);
            this.CountOfMarkersLabel.TabIndex = 11;
            this.CountOfMarkersLabel.Text = "Введите количество маркеров,необходимое для выхода из вершины";
            // 
            // GraphData
            // 
            this.GraphData.Location = new System.Drawing.Point(179, 97);
            this.GraphData.Multiline = true;
            this.GraphData.Name = "GraphData";
            this.GraphData.Size = new System.Drawing.Size(100, 107);
            this.GraphData.TabIndex = 12;
            // 
            // _zedGraph_
            // 
            this._zedGraph_.Location = new System.Drawing.Point(388, 243);
            this._zedGraph_.Name = "_zedGraph_";
            this._zedGraph_.ScrollGrace = 0D;
            this._zedGraph_.ScrollMaxX = 0D;
            this._zedGraph_.ScrollMaxY = 0D;
            this._zedGraph_.ScrollMaxY2 = 0D;
            this._zedGraph_.ScrollMinX = 0D;
            this._zedGraph_.ScrollMinY = 0D;
            this._zedGraph_.ScrollMinY2 = 0D;
            this._zedGraph_.Size = new System.Drawing.Size(182, 130);
            this._zedGraph_.TabIndex = 13;
            // 
            // DataListBox
            // 
            this.DataListBox.FormattingEnabled = true;
            this.DataListBox.Location = new System.Drawing.Point(246, 303);
            this.DataListBox.Name = "DataListBox";
            this.DataListBox.Size = new System.Drawing.Size(120, 95);
            this.DataListBox.TabIndex = 14;
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Image = global::WF.Properties.Resources.deleteAll;
            this.deleteALLButton.Location = new System.Drawing.Point(12, 258);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(54, 50);
            this.deleteALLButton.TabIndex = 19;
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::WF.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(12, 198);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(54, 54);
            this.deleteButton.TabIndex = 18;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = global::WF.Properties.Resources.edge;
            this.drawEdgeButton.Location = new System.Drawing.Point(12, 141);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(54, 51);
            this.drawEdgeButton.TabIndex = 17;
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Image = global::WF.Properties.Resources.vertex;
            this.drawVertexButton.Location = new System.Drawing.Point(12, 77);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(54, 51);
            this.drawVertexButton.TabIndex = 16;
            this.drawVertexButton.UseVisualStyleBackColor = true;
            this.drawVertexButton.Click += new System.EventHandler(this.drawVertexButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Image = global::WF.Properties.Resources.cursor;
            this.selectButton.Location = new System.Drawing.Point(12, 12);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(54, 59);
            this.selectButton.TabIndex = 15;
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // sheet
            // 
            this.sheet.Location = new System.Drawing.Point(86, 12);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(415, 345);
            this.sheet.TabIndex = 20;
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // ReadyButton
            // 
            this.ReadyButton.Location = new System.Drawing.Point(165, 363);
            this.ReadyButton.Name = "ReadyButton";
            this.ReadyButton.Size = new System.Drawing.Size(75, 23);
            this.ReadyButton.TabIndex = 21;
            this.ReadyButton.Text = "Далее";
            this.ReadyButton.UseVisualStyleBackColor = true;
            this.ReadyButton.Click += new System.EventHandler(this.ReadyButton_Click);
            // 
            // graphTypeButton
            // 
            this.graphTypeButton.Location = new System.Drawing.Point(152, 197);
            this.graphTypeButton.Name = "graphTypeButton";
            this.graphTypeButton.Size = new System.Drawing.Size(96, 23);
            this.graphTypeButton.TabIndex = 22;
            this.graphTypeButton.Text = "Графический";
            this.graphTypeButton.UseVisualStyleBackColor = true;
            this.graphTypeButton.Click += new System.EventHandler(this.graphTypeButton_Click);
            // 
            // textTypeButton
            // 
            this.textTypeButton.Location = new System.Drawing.Point(345, 197);
            this.textTypeButton.Name = "textTypeButton";
            this.textTypeButton.Size = new System.Drawing.Size(75, 23);
            this.textTypeButton.TabIndex = 23;
            this.textTypeButton.Text = "Текстовый\r\n";
            this.textTypeButton.UseVisualStyleBackColor = true;
            this.textTypeButton.Click += new System.EventHandler(this.textTypeButton_Click);
            // 
            // selectTypeLabel
            // 
            this.selectTypeLabel.AutoSize = true;
            this.selectTypeLabel.Location = new System.Drawing.Point(199, 162);
            this.selectTypeLabel.Name = "selectTypeLabel";
            this.selectTypeLabel.Size = new System.Drawing.Size(178, 13);
            this.selectTypeLabel.TabIndex = 24;
            this.selectTypeLabel.Text = "Выберите формат задания графа";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 314);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(54, 59);
            this.saveButton.TabIndex = 25;
            this.saveButton.Text = "Сохранить граф";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadFromFile
            // 
            this.loadFromFile.Location = new System.Drawing.Point(246, 234);
            this.loadFromFile.Name = "loadFromFile";
            this.loadFromFile.Size = new System.Drawing.Size(93, 40);
            this.loadFromFile.TabIndex = 26;
            this.loadFromFile.Text = "Загрузить граф из файла";
            this.loadFromFile.UseVisualStyleBackColor = true;
            this.loadFromFile.Click += new System.EventHandler(this.loadFromFile_Click);
            // 
            // saveGraphic
            // 
            this.saveGraphic.Location = new System.Drawing.Point(476, 332);
            this.saveGraphic.Name = "saveGraphic";
            this.saveGraphic.Size = new System.Drawing.Size(68, 41);
            this.saveGraphic.TabIndex = 27;
            this.saveGraphic.Text = "Сохранить график";
            this.saveGraphic.UseVisualStyleBackColor = true;
            this.saveGraphic.Click += new System.EventHandler(this.saveGraphic_Click);
            // 
            // OneMarkerButton
            // 
            this.OneMarkerButton.Location = new System.Drawing.Point(152, 195);
            this.OneMarkerButton.Name = "OneMarkerButton";
            this.OneMarkerButton.Size = new System.Drawing.Size(96, 60);
            this.OneMarkerButton.TabIndex = 28;
            this.OneMarkerButton.Text = "По 1 на ребро";
            this.OneMarkerButton.UseVisualStyleBackColor = true;
            this.OneMarkerButton.Click += new System.EventHandler(this.OneMarkerButton_Click);
            // 
            // someMarkersButton
            // 
            this.someMarkersButton.Location = new System.Drawing.Point(320, 195);
            this.someMarkersButton.Name = "someMarkersButton";
            this.someMarkersButton.Size = new System.Drawing.Size(125, 60);
            this.someMarkersButton.TabIndex = 29;
            this.someMarkersButton.Text = "Выпустить несколько импульсов на конкретное ребро";
            this.someMarkersButton.UseVisualStyleBackColor = true;
            this.someMarkersButton.Click += new System.EventHandler(this.someMarkersButton_Click);
            // 
            // SelectMarkersLabel
            // 
            this.SelectMarkersLabel.AutoSize = true;
            this.SelectMarkersLabel.Location = new System.Drawing.Point(185, 128);
            this.SelectMarkersLabel.Name = "SelectMarkersLabel";
            this.SelectMarkersLabel.Size = new System.Drawing.Size(239, 13);
            this.SelectMarkersLabel.TabIndex = 30;
            this.SelectMarkersLabel.Text = "Выберите изначальное количество маркеров";
            // 
            // Visualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 398);
            this.Controls.Add(this.SelectMarkersLabel);
            this.Controls.Add(this.someMarkersButton);
            this.Controls.Add(this.OneMarkerButton);
            this.Controls.Add(this.saveGraphic);
            this.Controls.Add(this.loadFromFile);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.selectTypeLabel);
            this.Controls.Add(this.textTypeButton);
            this.Controls.Add(this.graphTypeButton);
            this.Controls.Add(this.ReadyButton);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.deleteALLButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.drawEdgeButton);
            this.Controls.Add(this.drawVertexButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.DataListBox);
            this.Controls.Add(this._zedGraph_);
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
            this.Load += new System.EventHandler(this.Visualization_Load);
            this.Resize += new System.EventHandler(this.Visualization_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private ZedGraph.ZedGraphControl zedGraph;
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
        private ZedGraph.ZedGraphControl _zedGraph_;
        private System.Windows.Forms.ListBox DataListBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Button ReadyButton;
        private System.Windows.Forms.Button graphTypeButton;
        private System.Windows.Forms.Button textTypeButton;
        private System.Windows.Forms.Label selectTypeLabel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadFromFile;
        private System.Windows.Forms.Button saveGraphic;
        private System.Windows.Forms.Button OneMarkerButton;
        private System.Windows.Forms.Button someMarkersButton;
        private System.Windows.Forms.Label SelectMarkersLabel;
    }
}

