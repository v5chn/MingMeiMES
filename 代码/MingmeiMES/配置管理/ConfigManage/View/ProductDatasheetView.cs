﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ModuleCrossPnP;
using FTDataAccess;
using LogInterface;
using FTDataAccess.Model;
using FTDataAccess.BLL;
namespace ConfigManage
{
    enum EnumCmd
    {
        空,
        增加配置,
        修改配置
    }
    public partial class ProductDatasheetView : BaseChildView, IProductDataSheetView
    {
        #region 私有数据
        ProductDataPresenter presenter = null;
        private EnumCmd curCmd = EnumCmd.空;
        private ProductSizeCfgModel productCfgModel = new ProductSizeCfgModel();
        private ProductSizeCfgBll productCfgBll = null;
        private BindingSource productCfgBs = new BindingSource();
        //private ProductHeightDefBll heightDefBll = null;
        //private ProductPacsizeDefBll packsizeDefBll = null;
        //private ProductSizeCfgBll productCfgBll = null;
        #endregion
        #region 公共接口
        // public string CaptionText { get { return captionText; } set { captionText = value; this.Text = captionText; } }
        public ProductDatasheetView(string captionText):base(captionText)
        {
            InitializeComponent();
            this.Text = captionText;
            //this.captionText = captionText;
            productCfgBll = new ProductSizeCfgBll();
        }
        public void InitView()
        {
            presenter.SetLogRecorder(logRecorder);
           
            this.cbx_sizeCata.Items.AddRange(new string[] {  "产品包装尺寸字典" });
            this.cbx_sizeCata.SelectedIndex = 0;
            //绑定
            this.textBoxBarcode.DataBindings.Add("Text", this.productCfgModel, "productCataCode",false,DataSourceUpdateMode.OnPropertyChanged);
            this.cbx_Height.DataBindings.Add("Text", this.productCfgModel, "productHeight", false, DataSourceUpdateMode.OnPropertyChanged);
            this.cbx_Packsize.DataBindings.Add("Text", this.productCfgModel, "packageSize", false, DataSourceUpdateMode.OnPropertyChanged);
           
            this.textBoxProductName.DataBindings.Add("Text", this.productCfgModel, "productName",false,DataSourceUpdateMode.OnPropertyChanged);
            this.richTextBoxProductMark.DataBindings.Add("Text", this.productCfgModel, "mark", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxCataseq.DataBindings.Add("Text", this.productCfgModel, "cataSeq", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxDesignPower.DataBindings.Add("Text", this.productCfgModel, "power1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.textBoxRobotProg.DataBindings.Add("Text", this.productCfgModel, "robotProg", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        #endregion
        #region IProductDataSheetView接口实现
        public void DispHeightDeflist(DataTable dt)
        {
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["productHeight"].HeaderText = "产品高度";
            this.dataGridView1.Columns["heightSeq"].HeaderText = "编号";
            this.dataGridView1.Columns["mark"].HeaderText = "备注";
        }
        public void DispPacksizeDeflist(DataTable dt)
        {
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns["packageSize"].HeaderText = "包装尺寸";
            this.dataGridView1.Columns["packageSeq"].HeaderText = "编号";
            this.dataGridView1.Columns["mark"].HeaderText = "备注";
        }
        public void DispProductCfgList(DataTable dt)
        {

            productCfgBs.DataSource = dt;
            bindingNavigator1.BindingSource = productCfgBs;
            this.dataGridView2.DataSource = productCfgBs;
            for (int i = 1; i < 6; i++)
            {
                this.dataGridView2.Columns["tag" + i.ToString()].Visible = false;
            }

            this.dataGridView2.Columns["gasName"].Visible = false;
            this.dataGridView2.Columns["productCataCode"].HeaderText = "物料码";
            this.dataGridView2.Columns["cataSeq"].HeaderText = "物料编号";
            this.dataGridView2.Columns["productName"].HeaderText = "型号名称";
            this.dataGridView2.Columns["productHeight"].HeaderText = "产品高度";
            this.dataGridView2.Columns["productHeight"].Visible = false;
            this.dataGridView2.Columns["packageSize"].HeaderText = "包装箱尺寸";
            this.dataGridView2.Columns["robotProg"].HeaderText = "机器人配方";
            this.dataGridView2.Columns["mark"].HeaderText = "备注";
            this.dataGridView2.Columns["volt1"].Visible = false;
            this.dataGridView2.Columns["volt2"].Visible = false;
            this.dataGridView2.Columns["frequency1"].Visible = false;
            this.dataGridView2.Columns["frequency2"].Visible = false;

            //this.dataGridView2.Columns["volt1"].HeaderText = "强档电压";
            //this.dataGridView2.Columns["volt2"].HeaderText = "弱档电压";
            //this.dataGridView2.Columns["frequency1"].HeaderText = "强档频率";
           // this.dataGridView2.Columns["frequency2"].HeaderText = "弱档频率";
            //this.dataGridView2.Columns["frequency2"].Visible = false;
            this.dataGridView2.Columns["power1"].HeaderText = "额定功率";
            this.dataGridView2.Columns["power2"].Visible = false;

       
            this.dataGridView2.Columns["productName"].Width = 150;
            this.dataGridView2.Columns["productCataCode"].Width = 200;
            
          
        }
        public void ShowPopupMes(string mes)
        {
            MessageBox.Show(mes);
        }
        public int AskMessge(string mes)
        {
            DialogResult re = MessageBox.Show(mes,"提示",MessageBoxButtons.YesNo);
            if(re == DialogResult.Yes)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void RefreshHeightList(List<string> heightList)
        {
            this.cbx_Height.Items.Clear();
            this.cbx_Height.Items.AddRange(heightList.ToArray());
            this.cbx_Height.SelectedIndex = 0;
        }
        public void RefreshPacksizeList(List<string> packsizeList)
        {
            this.cbx_Packsize.Items.Clear();
            this.cbx_Packsize.Items.AddRange(packsizeList.ToArray());
            this.cbx_Packsize.SelectedIndex = 0;
        }
        public void RefreshGasList(List<string> gasList)
        {
           
        }
        #endregion
        #region UI事件
        private void btnDicitemAdd_Click(object sender, EventArgs e)
        {
            string dicCata = this.cbx_sizeCata.Text;
            ProductSizeDicDlg dlg = new ProductSizeDicDlg(dicCata,true);
            dlg.ShowDialog();
            presenter.RefreshList(this.cbx_sizeCata.Text);
        }

        private void ProductDatasheetView_Load(object sender, EventArgs e)
        {
            presenter = new ProductDataPresenter(this);
            InitView();
            
        }
        
        private void btnRefreshDic_Click(object sender, EventArgs e)
        {
            presenter.RefreshList(this.cbx_sizeCata.Text);
        }

        private void cbx_sizeCata_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.RefreshList(this.cbx_sizeCata.Text);
        }
        private void OnModifyDic()
        {
            string dicCata = this.cbx_sizeCata.Text;
            string[] dataArray = new string[3] { "", "", "" };
            if(this.dataGridView1.SelectedRows.Count<1)
            {
                MessageBox.Show("未选中待修改记录");
                return;
            }
            DataGridViewRow row=this.dataGridView1.SelectedRows[0];
            dataArray[0] = row.Cells[0].Value.ToString();
            dataArray[1] = row.Cells[1].Value.ToString();
            dataArray[2] = row.Cells[2].Value.ToString();
            
            ProductSizeDicDlg dlg = new ProductSizeDicDlg(dicCata, false);
            dlg.DataArray = dataArray;
            dlg.ShowDialog();
            presenter.RefreshList(this.cbx_sizeCata.Text);
            
        }
        private void btnDicitemModify_Click(object sender, EventArgs e)
        {
            OnModifyDic();
        }
        private void OnDelDic()
        {
            string dicCata = this.cbx_sizeCata.Text;
            
            if (this.dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("未选中待修改记录");
                return;
            }
            DataGridViewRow row = this.dataGridView1.SelectedRows[0];
            string key = row.Cells[0].Value.ToString();
            if(this.cbx_sizeCata.Text == "产品高度字典")
            {
                int hgtKey = int.Parse(key);
                ProductHeightDefBll hgtBll = new ProductHeightDefBll();
                hgtBll.Delete(hgtKey);
            }
            else
            {
                ProductPacsizeDefBll packSizeBll = new ProductPacsizeDefBll();
                packSizeBll.Delete(key);
            }
            presenter.RefreshList(this.cbx_sizeCata.Text);
        }
        private void btnDicitemDel_Click(object sender, EventArgs e)
        {
            OnDelDic();
        }
        private void btnRefreshProductCfg_Click(object sender, EventArgs e)
        {
            presenter.RefreshList("产品型号配置");
        }

        private void btnProductCfgAdd_Click(object sender, EventArgs e)
        {
            this.curCmd = EnumCmd.增加配置;
            this.panel2.Enabled = true;
            this.textBoxBarcode.Enabled = true;
            CfgFillClear();
            
            presenter.BeginAddProductCfg();
        }
        private void btnCfgModify_Click(object sender, EventArgs e)
        {
            this.curCmd = EnumCmd.修改配置;
            this.panel2.Enabled = true;
            CfgFillClear();
            presenter.BeginAddProductCfg();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(this.curCmd == EnumCmd.修改配置)
            {
                if (this.dataGridView2.SelectedRows.Count < 1)
                {
                    return;
                }
                DataGridViewRow row = this.dataGridView2.SelectedRows[0];
                // this.textBoxBarcode.Text = row.Cells["物料号"].Value.ToString();
                this.productCfgModel.productCataCode = row.Cells["productCataCode"].Value.ToString();
                // this.cbx_Height.Text = row.Cells["产品高度"].Value.ToString();
                this.productCfgModel.productHeight = int.Parse(row.Cells["productHeight"].Value.ToString());
                //this.cbx_Packsize.Text = row.Cells["包装尺寸"].Value.ToString();
                this.productCfgModel.packageSize = row.Cells["packageSize"].Value.ToString();
               
                // this.richTextBoxProductMark.Text = row.Cells["备注"].Value.ToString();
                this.productCfgModel.mark = row.Cells["mark"].Value.ToString();
                this.productCfgModel.productName = row.Cells["productName"].Value.ToString();
                this.productCfgModel.cataSeq = int.Parse(row.Cells["cataSeq"].Value.ToString());
                this.productCfgModel.robotProg = int.Parse(row.Cells["robotProg"].Value.ToString());
                this.productCfgModel.power1 = row.Cells["power1"].Value.ToString();

                this.textBoxBarcode.Enabled = false;
            }
           
        }

        private void buttonConfirmCfg_Click(object sender, EventArgs e)
        {
            if(this.curCmd == EnumCmd.修改配置)
            {
                presenter.ModifyProductCfg(productCfgModel);
            }
            else if(this.curCmd == EnumCmd.增加配置)
            {
                
                presenter.AddProductCfg(productCfgModel);
            }
        }

     
        private void buttonCancelCfg_Click(object sender, EventArgs e)
        {
            CfgFillClear();
            this.panel2.Enabled = false;
        }
        private void btnImportCfgs_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fd = new OpenFileDialog();  
            fd.Filter = "excel files (*.xlsx)|*.xlsx";
            if (fd.ShowDialog() == DialogResult.OK)  
            {  
               string fileName = fd.FileName;
               presenter.ImportProductCfgData(fileName);
            }  
        }

        private void btnDelSizeCfg_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.SelectedRows.Count < 1)
            {
                ShowPopupMes("未选中记录 ");
                return;
            }
            List<string> rds = new List<string>();
            foreach( DataGridViewRow row in this.dataGridView2.SelectedRows)
            {
                rds.Add( row.Cells["productCataCode"].Value.ToString());
            }
            presenter.DelProductCfg(rds);
        }
        private void CfgFillClear()
        {
            this.cbx_Packsize.Items.Clear();
            
            this.cbx_Height.Items.Clear();
            this.cbx_Height.Text = string.Empty;
            this.cbx_Packsize.Text = string.Empty;
            this.textBoxBarcode.Text = string.Empty;
            this.richTextBoxProductMark.Text = string.Empty;
            this.textBoxProductName.Text = string.Empty;
        }
        private void btnQueryProductCfg_Click(object sender, EventArgs e)
        {
            OnQueryProductCfg();
        }
        private void OnQueryProductCfg()
        {
            string productCata = this.textBox1QueryProduct.Text;
            string strWhere = string.Format("productCataCode='{0}' ", productCata);
            DataSet ds = productCfgBll.GetList(strWhere);
            DataTable dt=null;
            if(ds != null && ds.Tables.Count>0)
            {
                dt= ds.Tables[0];
               
            }
            DispProductCfgList(dt);
           // ProductSizeCfgModel productCfg = productCfgBll.GetModel(productCata);
            //foreach(DataGridViewRow dr in this.dataGridView2.Rows)
            //{
            //    if(dr.Cells["物料号"].Value.ToString() ==productCata)
            //    {
            //        dr.Selected = true;
            //        break;
            //    }
            //}
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = productCfgBll.GetAllList().Tables[0];
            Dictionary<string, string> dicColnames = new Dictionary<string, string>();
            dicColnames["productCataCode"] = "物料码";
            dicColnames["productName"] = "型号名称";
            dicColnames["cataSeq"] = "物料编号";
            dicColnames["packageSize"] = "包装尺寸";// + "." + dr.ItemArray[4].ToString()
           // dicColnames["productHeight"] = "产品高度";
            dicColnames["robotProg"] = "机器人配方编号";
           // dicColnames["volt1"] = @"强档电压";
           // dicColnames["volt2"] = "弱档电压";
          //  dicColnames["frequency1"] = "频率";
            dicColnames["power1"] = "合格功率";
            dicColnames["mark"] = "备注";
            List<string> exportCols = new List<string>();
            foreach(DataColumn dc in dt.Columns)
            {
                if(dicColnames.Keys.Contains(dc.ColumnName))
                {
                    dc.ColumnName = dicColnames[dc.ColumnName];
                    exportCols.Add(dc.ColumnName);
                }
               
            }
            ExportDtToExcel(dt,exportCols,"烟机线型号数据");
        }
        #endregion

        private void label6_Click(object sender, EventArgs e)
        {

        }

    }
}
