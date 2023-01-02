﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.IO;

public partial class RPT_PDC_Cheque_Report : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            //FillDDL_AcadYear();
        }
    }
    #endregion

    #region Methods


    /// <summary>
    /// Fill Division drop down list
    /// </summary>
    private void FillDDL_Division()
    {

        try
        {

            Clear_Error_Success_Box();
            //string CreatedBy = null;
            //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //CreatedBy = cookie.Values["UserID"];

            //if (string.IsNullOrEmpty(CreatedBy))
            //    Response.Redirect("Login.aspx");

            DataSet dsDivision = ProductController.GetAllActiveDivision_Zone_Center("1", "");
            BindDDL(ddlDivision, dsDivision, "Source_Division_ShortDesc", "Source_Division_Code");
            ddlDivision.Items.Insert(0, "Select");
            ddlDivision.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }

    /// <summary>
    /// Fill Academic Year dropdown
    /// </summary>
    //private void FillDDL_AcadYear()
    //{
    //    try
    //    {
    //        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
    //        BindDDL(ddlAcademicYear, dsAcadYear, "Description", "Id");
    //        ddlAcademicYear.Items.Insert(0, "Select");
    //        ddlAcademicYear.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {

    //        Msg_Error.Visible = true;
    //        Msg_Success.Visible = false;
    //        lblerror.Text = ex.ToString();
    //        UpdatePanelMsgBox.Update();
    //        return;
    //    }
    //}

    /// <summary>
    /// Fill Course dropdownlist 
    /// </summary>



    /// <summary>
    /// Fill Centers Based on login user 
    /// </summary>
    private void FillDDL_Centre()
    {
        try
        {
            //string CreatedBy = null;
            //HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            //CreatedBy = cookie.Values["UserID"];

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            DataSet dsCentre = ProductController.GetAllActiveDivision_Zone_Center("2", Div_Code);
            BindListBox(ddlCentre, dsCentre, "Source_Center_Name", "Source_Center_Code");
            ddlCentre.Items.Insert(0, "Select");
            ddlCentre.Items.Insert(1, "All");
            ddlCentre.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }







    /// <summary>
    /// Fill drop down list and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    /// <summary>
    /// Fill List box and assign value and Text
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="ds"></param>
    /// <param name="txtField"></param>
    /// <param name="valField"></param>
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    /// <summary>
    /// Clear Error Success Box
    /// </summary>
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }


    /// <summary>
    /// Show Error or success box on top base on boxtype and Error code
    /// </summary>
    /// <param name="BoxType">BoxType</param>
    /// <param name="Error_Code">Error_Code</param>
    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }





    /// <summary>
    /// Bind search  Datalist
    /// </summary>
    private void FillGrid()
    {
        try
        {
            Clear_Error_Success_Box();

            if (ddlDivision.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Division");
                ddlDivision.Focus();
                return;
            }
   

            if (ddlCentre.SelectedItem.ToString() == "Select")
            {
                Show_Error_Success_Box("E", "Select Center");
                ddlCentre.Focus();
                return;
            }
            string CenterCode = "";
            if (ddlCentre.SelectedItem.ToString() == "All")
             {
                 CenterCode = "All";
             }
            if (ddlCentre.SelectedItem.ToString() != "All")
            {
                for (int cnt = 0; cnt <= ddlCentre.Items.Count - 1; cnt++)
                {
                    if (ddlCentre.Items[cnt].Selected == true)
                    {
                        CenterCode = CenterCode + ddlCentre.Items[cnt].Value + ",";
                    }
                }
            }


            ControlVisibility("Result");
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

             string DateRange = "";
             DateRange = txtperiodCalendr.Value;

        if (DateRange.Length == 0)
        {

            Show_Error_Success_Box("E", "Select Date");
            //ddlCentre.Focus();
            return;
            
        }
        //string DateRange = txtperiodCalendr.Value;
        string FromDate = DateRange.Substring(0, 10);
        string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

        DataSet dsGrid = ProductController.Get_PDCchequereport(DivisionCode, FromDate, Todate, CenterCode);
        if (dsGrid.Tables[0].Rows.Count > 0)
        {

            dlGridDisplay.DataSource = dsGrid.Tables[0];
            dlGridDisplay.DataBind();


            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lbldaterange_Result.Text = txtperiodCalendr.Value;
            lblCenter_Result.Text = ddlCentre.SelectedItem.ToString();
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcount.Text = (dsGrid.Tables[0].Rows.Count).ToString();
                    }
                    else
                    {
                        lbltotalcount.Text = "0";
                    }
                }
                else
                {
                    lbltotalcount.Text = "0";
                }
            }
            else
            {
                lbltotalcount.Text = "0";
            }

        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "No Record Found";
            ControlVisibility("Search");

        }

        }
        catch (Exception ex)
        {

            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;
        }
    }




    #endregion



    #region Event's


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        //ddlAcademicYear.SelectedIndex = 0;
        ddlDivision_SelectedIndexChanged(sender, e);
    }



    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }



    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivSearchPanel.Visible = true;
            DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivSearchPanel.Visible = false;
            DivResultPanel.Visible = true;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //string Date = Text1.Value;
        FillGrid();
    }



    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ddlCentre.Items.Clear();

        FillDDL_Centre();




    }

    //protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Clear_Error_Success_Box();
    //    if (ddlDivision.SelectedItem.ToString() == "Select")
    //    {

    //        ddlDivision.Focus();
    //        return;
    //    }
    //    if (ddlAcademicYear.SelectedItem.ToString() == "Select")
    //    {

    //        ddlAcademicYear.Focus();
    //        return;
    //    }


    //}



    protected void HLExport_Click(object sender, EventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Cheque_Report_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridDisplay.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }


    #endregion
}