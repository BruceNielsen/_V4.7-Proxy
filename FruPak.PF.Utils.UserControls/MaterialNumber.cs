using System;
using System.Data;
using System.Windows.Forms;

namespace FruPak.PF.Utils.UserControls
{
    public partial class MaterialNumber : UserControl
    {
        public int selectedTrader
        {
            get;
            set;
        }

        public int selectedProductGroup
        {
            get;
            set;
        }

        public int selectedPackType
        {
            get;
            set;
        }

        public int selectedBrand
        {
            get;
            set;
        }

        public decimal selectedWeight
        {
            get;
            set;
        }

        public int selectedGrade
        {
            get;
            set;
        }

        public int selectedTreatment
        {
            get;
            set;
        }

        public int selectedMaterial
        {
            get;
            set;
        }

        //public int selectedMaterial
        //{
        //    get
        //    {
        //        if (cmb_Material.SelectedValue == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt32(cmb_Material.SelectedValue.ToString());
        //        }
        //    }
        //    set
        //    {
        //        if (value == 0)
        //        {
        //            cmb_Material.SelectedIndex = -1;
        //        }
        //        else
        //        {
        //            cmb_Material.SelectedIndex = value;
        //            dv.RowFilter = string.Format("Material_Id = {0}", value);
        //            populate();
        //        }
        //    }
        //}
        public int selectedVariety
        {
            get;
            set;
        }

        public int selectedGrowingMethod
        {
            get;
            set;
        }

        public int selectedSize
        {
            get;
            set;
        }

        public MaterialNumber()
        {
            InitializeComponent();

            //       populate_comboboxs();
        }

        private DataView dv;

        public void populate()
        {
            DataSet ds_Get_Info;
            ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info_By_Translated();
            dv = new DataView(ds_Get_Info.Tables[0]);
            string filterString = "";
            if (selectedVariety != 0)
                filterString = string.Format("Variety_Id = {0}", selectedVariety);
            if (selectedGrowingMethod != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Growing_Method_Id = {0}", selectedGrowingMethod);
            }
            if (selectedProductGroup != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("ProductGroup_Id = {0}", selectedProductGroup);
            }
            if (selectedTrader != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Trader_Id = {0}", selectedTrader);
            }
            if (selectedBrand != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Brand_Id = {0}", selectedBrand);
            }
            if (selectedMaterial != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Material_Id = {0}", selectedMaterial);
            }
            if (selectedPackType != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("PackType_Id = {0}", selectedPackType);
            }
            if (selectedWeight != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Weight = {0}", selectedWeight);
            }
            if (selectedGrade != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Grade_Id = {0}", selectedGrade);
            }
            if (selectedTreatment != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Treatment_Id = {0}", selectedTreatment);
            }
            if (selectedSize != 0)
            {
                if (filterString != "")
                    filterString = filterString + " AND ";
                filterString = filterString + string.Format("Size_Id = {0}", selectedSize);
            }
            dv.RowFilter = filterString;

            //Trader
            cmb_Trader.DataSource = dv.ToTable(true, "T_Combined", "Trader_Id");
            cmb_Trader.DisplayMember = "T_Combined";
            cmb_Trader.ValueMember = "Trader_Id";
            cmb_Trader.SelectedValue = selectedTrader;
            if (cmb_Trader.Items.Count == 1)
                cmb_Trader.SelectedIndex = 0;

            //Product Group

            cmb_Product_Group.DataSource = dv.ToTable(true, "PG_Combined", "ProductGroup_Id");
            cmb_Product_Group.DisplayMember = "PG_Combined";
            cmb_Product_Group.ValueMember = "ProductGroup_Id";
            cmb_Product_Group.SelectedValue = selectedProductGroup;
            if (cmb_Product_Group.Items.Count == 1)
                cmb_Product_Group.SelectedIndex = 0;

            //pack Type
            cmb_PackType.DataSource = dv.ToTable(true, "PT_Combined", "PackType_Id");
            cmb_PackType.DisplayMember = "PT_Combined";
            cmb_PackType.ValueMember = "PackType_Id";
            cmb_PackType.SelectedValue = selectedPackType;
            if (cmb_PackType.Items.Count == 1)
                cmb_PackType.SelectedIndex = 0;

            //Brand
            cmb_Brand.DataSource = dv.ToTable(true, "B_Combined", "Brand_Id");
            cmb_Brand.DisplayMember = "B_Combined";
            cmb_Brand.ValueMember = "Brand_Id";
            cmb_Brand.SelectedValue = selectedBrand;
            if (cmb_Brand.Items.Count == 1)
                cmb_Brand.SelectedIndex = 0;

            //Weight
            cmb_Weight.DataSource = dv.ToTable(true, "Weight");
            cmb_Weight.DisplayMember = "Weight";
            cmb_Weight.ValueMember = "Weight";
            cmb_Weight.SelectedValue = selectedWeight;
            if (cmb_Weight.Items.Count == 1)
                cmb_Weight.SelectedIndex = 0;

            //Grade
            cmb_Grade.DataSource = dv.ToTable(true, "G_Combined", "Grade_Id");
            cmb_Grade.DisplayMember = "G_Combined";
            cmb_Grade.ValueMember = "Grade_Id";
            cmb_Grade.SelectedValue = selectedGrade;
            if (cmb_Grade.Items.Count == 1)
                cmb_Grade.SelectedIndex = 0;

            //Treatment
            cmb_Treatment.DataSource = dv.ToTable(true, "TM_Combined", "Treatment_Id");
            cmb_Treatment.DisplayMember = "TM_Combined";
            cmb_Treatment.ValueMember = "Treatment_Id";
            cmb_Treatment.SelectedValue = selectedTreatment;
            if (cmb_Treatment.Items.Count == 1)
                cmb_Treatment.SelectedIndex = 0;

            //Size
            cmb_Size.DataSource = dv.ToTable(true, "S_Combined", "Size_Id");
            cmb_Size.DisplayMember = "S_Combined";
            cmb_Size.ValueMember = "Size_Id";
            cmb_Size.SelectedValue = selectedSize;
            if (cmb_Size.Items.Count == 1)
                cmb_Size.SelectedIndex = 0;

            //Material
            cmb_Material.DataSource = dv;
            cmb_Material.DisplayMember = "Material_Num";
            cmb_Material.ValueMember = "Material_Id";
            cmb_Material.SelectedValue = selectedMaterial;
            if (cmb_Material.Items.Count == 1)
                cmb_Material.SelectedIndex = 0;
            ds_Get_Info.Dispose();
        }

        private void cmb_Trader_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedTrader = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Trader_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Product_Group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedProductGroup = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("ProductGroup_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_PackType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedPackType = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("PackType_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Brand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedBrand = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Brand_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Weight_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedWeight = Convert.ToDecimal((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Weight = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Grade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedGrade = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Grade_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Treatment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedTreatment = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Treatment_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Size_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedSize = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Size_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        private void cmb_Material_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                selectedMaterial = Convert.ToInt32((sender as ComboBox).SelectedValue.ToString());
                dv.RowFilter = string.Format("Material_Id = {0}", (sender as ComboBox).SelectedValue);
                populate();
            }
        }

        /// <summary>
        /// Allows program to change the selected material and have everything update as if the user had selected a
        /// material number.
        /// </summary>
        /// <param name="selectedMaterial"></param>
        public void setMaterial(string chosenMaterial)
        {
            if (chosenMaterial != "")
            {
                // Need to convert Material_Num to Material_Id to set selectedMaterial
                DataSet ds_Get_Info;
                DataRow dr_get_info;
                ds_Get_Info = FruPak.PF.Data.AccessLayer.CM_Material.Get_Info(Convert.ToDecimal(chosenMaterial));
                for (int i = 0; i < Convert.ToInt32(ds_Get_Info.Tables[0].Rows.Count.ToString()); i++)
                {
                    dr_get_info = ds_Get_Info.Tables[0].Rows[i];
                    selectedMaterial = Convert.ToInt32(dr_get_info["Material_Id"].ToString());
                }

                dv.RowFilter = string.Format("Material_Num = {0}", chosenMaterial);
                populate();
            }
        }

        public void setMaterial(int chosenMaterialId)
        {
            selectedMaterial = chosenMaterialId;
            dv.RowFilter = string.Format("Material_Num = {0}", chosenMaterialId);
            populate();
        }

        // resets all combo boxes
        public void Reset()
        {
            selectedProductGroup = 0;
            selectedMaterial = 0;
            selectedPackType = 0;
            selectedTrader = 0;
            selectedWeight = 0;
            selectedBrand = 0;
            selectedGrade = 0;
            selectedTreatment = 0;
            selectedSize = 0;
            populate();
        }

        // set and get for the Material number for the selected item.
        public int Material_Number
        {
            get
            {
                if (cmb_Material.SelectedItem != null)
                {
                    return Convert.ToInt32(((DataRowView)cmb_Material.SelectedItem).Row.ItemArray[0].ToString());
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    cmb_Material.SelectedIndex = -1;
                }
                else
                {
                    cmb_Material.Text = Convert.ToString(value);
                }
            }
        }
    }
}