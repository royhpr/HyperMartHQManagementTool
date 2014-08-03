﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hypermarket_Admin_Management_Tool._0_View
{
    public partial class Main : Form
    {
        private _2_Controller.CoordinatingController mainController = _2_Controller.CoordinatingController.getInstance();
        private string functionTab;
        
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            UpdateGreeting(GlobalVariableAccessor.PasswordValidDays);
            DetermineVisibilityOfFunctionBlocks();
        }

        #region switch different interface thread

        public void enterOperationInterfaceThread()
        {
            Application.Run(new Operation(functionTab));
        }

        public static void logoutThread()
        {
            Application.Run(new _0_View.Login());
        }

        #endregion

        
        #region Click events

        private void pbManageShops_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_SHOP);
        }

        private void pbManageProduct_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_PRODUCT);
        }

        private void pbManageStock_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_STOCK);
        }

        private void pbViewReport_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_REPORT);
        }

        private void pbPendingOrder_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_ORDER);
        }

        private void pbViewManufacturerList_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_MANUFACTURER);
        }

        private void pbViewStaffList_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_STAFF);
        }

        private void pbDiscountPolicy_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_DISCOUNT);
        }

        private void pbProfileSetting_Click(object sender, EventArgs e)
        {
            openOperationInterfaceAndSetTabName(Constant.OPERATION_TAB_PROFILE);
        }

        private void llblChangePsw_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword frmChangePsw = new ChangePassword();
            frmChangePsw.ShowDialog();
            UpdateGreeting(GlobalVariableAccessor.PasswordValidDays);
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pbLogout_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(logoutThread));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            this.Close();
        }

        #endregion

        public void UpdateGreeting(string days)
        {
            lblGreeting.Text = Constant.GREETING0 + GlobalVariableAccessor.Salution + GlobalVariableAccessor.CurrentStaffName + Constant.COMMA + Constant.GREETING1 + days + Constant.DAYS;
        }

        private void openOperationInterfaceAndSetTabName(string tabName)
        {
            functionTab = tabName;
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(enterOperationInterfaceThread));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();

            this.Close();
        }

        private void ChangeBackGroundColor(object sender, EventArgs e)
        {
            PictureBox currentBox = sender as PictureBox;
            currentBox.BackColor = Color.FromArgb(0, 0, 64);
        }

        private void ResetBackGroundColor(object sender, EventArgs e)
        {
            PictureBox currentBox = sender as PictureBox;
            currentBox.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void GreyOutCurrentBlock(object sender, EventArgs e)
        {
            PictureBox currentBlock = sender as PictureBox;

            currentBlock.BorderStyle = BorderStyle.None;
            currentBlock.BackColor = Color.DimGray;
        }  

        private void DetermineVisibilityOfFunctionBlocks()
        {
            string position = GlobalVariableAccessor.Position;
            switch (position)
            {
                case Constant.ADMIN:
                    SetPanelsVisibility(true,true,true,true,true,true,true,true,true);
                    break;
                case Constant.PRODUCT_MANAGER:
                case Constant.PRODUCT_OFFICER:
                    SetPanelsVisibility(false,true,false,false,false,true,false,false,true);
                    break;
                case Constant.SALES_MANAGER:
                case Constant.SALES_OFFICER:
                    SetPanelsVisibility(false,false,false,true,false,false,false,true,true);
                    break;
                case Constant.WAREHOUSE_MANAGER:
                case Constant.WAREHOUSE_OFFICER:
                    SetPanelsVisibility(true,false,true,false,true,false,false,false,true);
                    break;

                default:
                    break;
            }
        }

        private void SetPanelsVisibility(bool shop, bool product, bool stock, bool report, bool request, bool manufacturer, bool staff, bool discount, bool profile)
        {
            pbManageShops.Enabled = shop;
            pbManageProduct.Enabled = product;
            pbManageStock.Enabled = stock;
            pbViewReport.Enabled = report;
            pbPendingOrder.Enabled = request;
            pbViewManufacturerList.Enabled = manufacturer;
            pbViewStaffList.Enabled = staff;
            pbDiscountPolicy.Enabled = discount;
            pbProfileSetting.Enabled = profile;
        }
    }
}
