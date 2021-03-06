﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class corporate_DalmiaCement : System.Web.UI.Page
{
    SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LeadTracker lt = new LeadTracker();

            if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["dalmiamailerid"])))
            {
                string[] str = Convert.ToString(Request.QueryString["dalmiamailerid"]).Split('?');
                if (str[0] == "b5ad15dad7d14c00a5d9a2729bf18577")
                {
                    if (Session["userSessionID"] == null)
                    {
                        lt.TrackLead("dalmiamailerid", "b5ad15dad7d14c00a5d9a2729bf18577", "Dalmia_Google_Mailer");
                        Session["ccode"] = "C22";
                        Session["ckey"] = "b5ad15dad7d14c00a5d9a2729bf18577";
                    }
                    else
                    {
                        lt.campaign_lead_capture(Session["userSessionID"].ToString(), "b5ad15dad7d14c00a5d9a2729bf18577");
                        Session["Campaign"] = "b5ad15dad7d14c00a5d9a2729bf18577";
                        Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
                    }
                }
                else
                {
                    if (Session["userSessionID"] == null)
                    {
                        lt.TrackLead("dalmiapageid", "629b31a3617547bb936fa8fdf75abaa8", "Dalmia_Landing_Page");
                        Session["ccode"] = "C23";
                        Session["ckey"] = "629b31a3617547bb936fa8fdf75abaa8";
                    }
                    else
                    {
                        lt.campaign_lead_capture(Session["userSessionID"].ToString(), "629b31a3617547bb936fa8fdf75abaa8");
                        Session["Campaign"] = "629b31a3617547bb936fa8fdf75abaa8";
                        Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
                    }
                }
            }
            else
            {
                if (Session["userSessionID"] == null)
                {
                    lt.TrackLead("dalmiapageid", "629b31a3617547bb936fa8fdf75abaa8", "Dalmia_Landing_Page");
                    Session["ccode"] = "C23";
                    Session["ckey"] = "629b31a3617547bb936fa8fdf75abaa8";
                }
                else
                {
                    lt.campaign_lead_capture(Session["userSessionID"].ToString(), "629b31a3617547bb936fa8fdf75abaa8");
                    Session["Campaign"] = "629b31a3617547bb936fa8fdf75abaa8";
                    Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
                }
            }
        }
    }
    protected void btn_submit1_Click(object sender, EventArgs e)
    {
        string usr = usermanagement(Name1.Value, Email1.Value, Mobile1.Value, Password1.Value, "R", "lit_sucess1");
        if (usr == "ok")
        {
            Name1.Value = string.Empty;
            Email1.Value = string.Empty;
            Mobile1.Value = string.Empty;
            Password1.Value = string.Empty;
            lit_sucess1.Visible = false; ;
            Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
        }
        else
        {
            lit_sucess1.Visible = true;
            lit_sucess1.Text = usr;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string usr = usermanagement(Name.Value, Email.Value, Mobile.Value, password.Value, "R", "lit_sucess");
        if (usr == "ok")
        {
            Name.Value = string.Empty;
            Email.Value = string.Empty;
            Mobile.Value = string.Empty;
            password.Value = string.Empty;
            lit_sucess.Visible = false; ;
            Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
        }
        else
        {
            lit_sucess.Visible = true;
            lit_sucess.Text = usr;
        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        LeadTracker lt = new LeadTracker();
        string usr = usermanagement("", eusername.Text.Trim(), "", epassword.Text.Trim(), "L", hfval.Value.Trim());
        if (usr == "ok")
        {
            eusername.Text = string.Empty;
            epassword.Text = string.Empty;
            Response.Redirect("../SFinancialPlanning/src/Questions.aspx");
        }
        else
        {
            if (hfval.Value.Trim() == "lit_sucess")
                lit_sucess.Text = usr;
            if (hfval.Value.Trim() == "lit_sucess1")
                lit_sucess1.Text = usr;
        }

    }
    protected void forgetButton_Click(object sender, EventArgs e)
    {
        LeadTracker lt = new LeadTracker();
        string usr = usermanagement("", txtEmailID.Text.Trim(), "", "", "F", hfval.Value.Trim());
        if (usr == "ok")
        {
            txtEmailID.Text = string.Empty;

            if (hfval.Value.Trim() == "lit_sucess")
                lit_sucess.Text = usr;
            if (hfval.Value.Trim() == "lit_sucess1")
                lit_sucess1.Text = usr;
        }
        else
        {
            if (hfval.Value.Trim() == "lit_sucess")
                lit_sucess.Text = usr;
            if (hfval.Value.Trim() == "lit_sucess1")
                lit_sucess1.Text = usr;
        }
    }
    string usermanagement(string name, string email, string mobile, string pass, string type, string hfval)
    {
        string res = "ok", ccode = Session["ccode"].ToString(), ckey = Session["ckey"].ToString();

        LeadTracker lt = new LeadTracker();

        try
        {
            if (type == "R")
            {
                if (lt.checkuserexsists(email.Trim()))
                {
                    res = "<div class='alert-danger' style='text-align: center; border-radius: 5px; margin-top: 2px;background-color:#fff;color:#4c9e32;' >Email ID already registered. <a href=javascript:openlogin('" + hfval.Trim() + "'); style='color:#1168d5'>Sign IN</a></div>";
                }
                else
                {
                    Session["userSessionID"] = lt.registeruser(name.Trim(), email.Trim(), pass.Trim(), mobile.Trim(), ccode);
                    lt.campaign_lead_capture(email.Trim(), ckey);
                    Session["Campaign"] = ckey;
                }
            }
            else if (type == "L")
            {
                string usr = lt.userlogin(email.Trim(), pass.Trim());
                if (!String.IsNullOrWhiteSpace(usr))
                {
                    Session["userSessionID"] = usr;
                    lt.campaign_lead_capture(usr, ckey);
                    Session["Campaign"] = ckey;
                }
                else
                {
                    res = "<div class='alert-danger' style='text-align: center; border-radius: 5px; margin-top: 2px;background-color:#fff;color:#ff0000;' >Invalid Credentials !! <a href=javascript:openlogin('" + hfval.Trim() + "'); style='color:#1168d5'>Sign IN</a></div>";
                }

            }
            else
            {
                if (lt.forgetpass(email.Trim()))
                {
                    res = "<div class='alert-danger' style='text-align: center; border-radius: 5px; margin-top: 2px;background-color:#fff;color:#4c9e32;' >We have sent information about your password to your recovery email address. <a href=javascript:openlogin('" + hfval.Trim() + "'); style='color:#1168d5'>Sign IN</a></div>";
                }
                else
                {
                    res = "<div class='alert-danger' style='text-align: center; border-radius: 5px; margin-top: 2px;background-color:#fff;color:#ff0000;' >Invalid Email !! <a href=javascript:openlogin('" + hfval.Trim() + "'); style='color:#1168d5'>Sign IN</a></div>";
                }
            }
        }
        catch
        {
            res = "error";
        }
        return res;
    }
}