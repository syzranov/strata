﻿namespace Strata
{
    partial class WorkArea
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WorkArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
            this.Name = "WorkArea";
            this.Size = new System.Drawing.Size(800, 800);
            this.ClientSizeChanged += new System.EventHandler(this.WorkArea_ClientSizeChanged);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.WorkArea_ControlRemoved);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WorkArea_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorkArea_MouseDown);
            this.MouseEnter += new System.EventHandler(this.WorkArea_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.WorkArea_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorkArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorkArea_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
