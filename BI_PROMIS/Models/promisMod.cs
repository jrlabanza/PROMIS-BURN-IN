using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BI_PROMIS.Models
{
    public class BIMod
    {

        public List<IDictionary<string, string>> show_bi_machines()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "SELECT * FROM testers_bi WHERE New_Tester_ID LIKE 'BI0%' OR New_Tester_ID LIKE 'RC%' OR New_Tester_ID LIKE 'AG%' OR New_Tester_ID LIKE 'MC%' OR New_Tester_ID LIKE 'BK%' OR New_Tester_ID LIKE 'TCT%' OR New_Tester_ID LIKE 'BH%' ORDER BY file_datetime asc";

            results = Connection.GetDataAssociateArray(query, "Show bi machinelist", Connection.promis_connString);

            return results;
        }

        //public List<IDictionary<string, string>> show_process(string location)
        //{
        //    List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

        //    string query = "select distinct process from testers WHERE Location ='" + location + "'";

        //    results = Connection.GetDataAssociateArray(query, "Show FOL machinelist", Connection.promis_connString);

        //    return results;
        //}


        public List<IDictionary<string, string>> show_statusowner()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select distinct owner from promis_code_owner1 order by owner ASC";

            results = Connection.GetDataAssociateArray(query, "Show Status Owner", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> show_testerPF()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select distinct TesterPF from testers_bi where New_Tester_ID LIKE 'BI0%' order by TesterPF ASC";

            results = Connection.GetDataAssociateArray(query, "Show Tester PF", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> show_testerID_onload()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select New_Tester_ID FROM testers_bi where New_Tester_ID LIKE 'BI0%'";

            results = Connection.GetDataAssociateArray(query, "Show Tester ID", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> show_testerID(string pfid)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select New_Tester_ID FROM testers_bi WHERE TesterPF ='" + pfid + "'";

            results = Connection.GetDataAssociateArray(query, "Show Tester ID", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> show_Data(int id)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "select * FROM testers_bi WHERE id =" + id + "";

            results = Connection.GetDataArray(query, "Show FOL Data", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> get_status_owner()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select distinct owner from promis_code_owner1 order by owner ASC";

            results = Connection.GetDataAssociateArray(query, "Get Status Owner", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> get_status(string stsOwner)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select description from promis_code_owner1 where owner ='" + stsOwner + "'order by owner asc";

            results = Connection.GetDataAssociateArray(query, "Get Status", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> get_uph(string machinePF, string family)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "select uph from p1_uph2 WHERE handler = '" + machinePF + "' and family = '" + family + "'";

            results = Connection.GetDataArray(query, "Get Family Type", Connection.promis_connString);

            return results;

        }

        public IDictionary<string, string> get_uph_check(string testerPF, string handlerPF, string family)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            //string query = "select uph from p1_uph2 WHERE TESTER = '"+ testerPF +"' AND HANDLER = '" + handlerPF + "' AND FAMILY = '" + family + "'";

            string query = "select uph from p1_uph2 WHERE HANDLER = '" + handlerPF + "' AND FAMILY = '" + family + "'";

            results = Connection.GetDataArray(query, "Get Family Type", Connection.promis_connString);

            return results;

        }

        public List<IDictionary<string, string>> get_device()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select distinct family from p1_uph2 order by family asc";

            results = Connection.GetDataAssociateArray(query, "Get Family Type", Connection.promis_connString);

            return results;

        }

        public IDictionary<string, string> get_package_type(string id)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "select Pkg_Type FROM p1_uph2 WHERE FAMILY ='" + id + "' LIMIT 1";

            results = Connection.GetDataArray(query, "Get Package Type", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> get_handler(string handlerModel)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select Equipt_Model, Equipt_ID from equipt_list where Prod_Line = 'Burn-in' AND Equipt_Model = '"+ handlerModel +"'";

            results = Connection.GetDataAssociateArray(query, "Get Platform Type", Connection.promis_connString);

            return results;

        }

        public IDictionary<string, string> get_handler_onLoad(string handlerID)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "select equipt_model,Equipt_PF from equipt_list where Equipt_ID = '" + handlerID + "' limit 1";

            results = Connection.GetDataArray(query, "Get Platform Type", Connection.promis_connString);

            return results;

        }

        public List<IDictionary<string, string>> get_prod_line()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select ProdLine from uph_prodline order by ProdLine asc";

            results = Connection.GetDataAssociateArray(query, "Get Production Line", Connection.promis_connString);

            return results;

        }

        public List<IDictionary<string, string>> show_filtered_machine_BI(string statOwner, string machine, string machineID)
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string GetstatOwner = "";
            string Getmachine = "";
            string GetmachineID = "";

            if (statOwner != "")
            {
                GetstatOwner = "AND Status_Owner = '" + statOwner + "'";
            }

            if (machine != "")
            {
                Getmachine = "AND TesterPF = '" + machine + "'";
            }

            if (machineID != "")
            {
                GetmachineID = "AND New_Tester_ID = '" + machineID + "'";
            }

            string query = "SELECT * from testers_bi where New_Tester_ID LIKE 'BI0%' " + GetstatOwner + " " + Getmachine + "  " + GetmachineID + " order by file_datetime asc";

            results = Connection.GetDataAssociateArray(query, "Show BI machinelist", Connection.promis_connString);

            return results;
        }

        ///

        public List<IDictionary<string, string>> get_all_user()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "select email_address from users ORDER BY first_name ASC";

            results = Connection.GetDataAssociateArray(query, "Get User", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> check_user(string user, string pass)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "select email_address,password from users WHERE email_address ='" + user + "'AND password ='" + pass + "'";

            results = Connection.GetDataArray(query, "Check User", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> check_track_by_machine(string machineID)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            //string query = "SELECT * FROM track_record WHERE lot_name = '"+ lotNo +"' AND machine = '"+ machineID +"'";
            string query = "SELECT * FROM track_record WHERE machine = '" + machineID + "' ORDER BY date_issued DESC LIMIT 1";

            results = Connection.GetDataArray(query, "Check track by machine", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> check_track_by_lot(string lotNo)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            //string query = "SELECT * FROM track_record WHERE lot_name = '"+ lotNo +"' AND machine = '"+ machineID +"'";
            string query = "SELECT * FROM track_record WHERE lot_name = '" + lotNo + "' ORDER BY date_issued DESC LIMIT 1";

            results = Connection.GetDataArray(query, "Check track by lot", Connection.promis_connString);

            return results;
        }

        public Boolean insert_Data(string machineID, string machinePF, string stsOwner, string stsDes, string prodName, string lotNo, string pkgType, string remarks, string group, string user,
            string date3, string pkgLine, string waferID, string set_temp, string actual_temp, string set_timer_1, string set_timer_2, string voltage_set, string chamber_condition,
            string wafer_size, string sampling_qty, string bi_aging_board_name, string device_qty_per_loading, string item_isolation, string handlerModel, string aging_tray_no,
            string ps_1, string ps_2, string ps_3, string clk_1, string clk_2, string clk_3, string clk_4, string clk_5, string clk_6, string clk_7, string clk_8, string no_of_running_slot, string max_no_of_slot, string burn_in_board_cleaning, string Handler_ID)
        {

            string query = @"UPDATE testers_bi set Device='" + prodName + "', pkg_type='" + pkgType + "', Lot_Name='" + lotNo + "', Curr_Status='" + stsDes +
                "', Status_Desc='" + stsDes + "', Status_Owner='" + stsOwner + "', Comments='" + remarks + "', who='" + user + "', file_datetime = '" + date3 +
                "', Prod_Line = '" + pkgLine + "', waferID = '" + waferID + "', set_temp = '"+ set_temp +"', actual_temp = '"+ actual_temp +"', set_timer_1 = '"+ set_timer_1 +
                "', set_timer_2 = '"+ set_timer_2 +"', voltage_set = '"+ voltage_set +"', chamber_condition = '"+ chamber_condition +"', wafer_size = '"+ wafer_size +
                "', sampling_qty = '" + sampling_qty + "', bi_aging_board_name = '" + bi_aging_board_name + "', device_qty_per_loading = '" + device_qty_per_loading +
                "', item_isolation = '" + item_isolation + "', handler_model = '" + handlerModel + "', aging_tray_no = '" + aging_tray_no + "', ps_1 = '" + ps_1 +
                "', ps_2 = '" + ps_2 + "' , ps_3 = '" + ps_3 + "', clk_1 = '" + clk_1 + "', clk_2 = '" + clk_2 + "', clk_3 = '" + clk_3 +
                "', clk_4 = '"+ clk_4 +"', clk_5 = '"+ clk_5 +"', clk_6 = '"+ clk_6 +"', clk_7 = '"+ clk_7 +"', clk_8 ='"+ clk_8 +"' ,no_of_running_slot = '" + no_of_running_slot +
                "', max_no_of_slot = '" + max_no_of_slot + "', burn_in_board_cleaning = '" + burn_in_board_cleaning + "', Handler_ID = '"+ Handler_ID +"' WHERE TesterID='" + machineID + "'";

            Boolean results = Connection.ExecuteThisQuery(query, "Get User", Connection.promis_connString);

            return results;
        }

        public Boolean insert_Data_History(string Handler_ID, string stsDes, string machineID, string prodName, string remarks, string user, string date1, string date2, string set_temp, string actual_temp, string set_timer_1,
            string set_timer_2, string voltage_set, string chamber_condition, string wafer_size, string sampling_qty, string bi_aging_board_name, string device_qty_per_loading, string item_isolation,
            string handlerModel, string aging_tray_no, string ps_1, string ps_2, string ps_3, string clk_1, string clk_2, string clk_3, string clk_4, string clk_5, string clk_6, string clk_7, string clk_8, string no_of_running_slot, string max_no_of_slot, string burn_in_board_cleaning)
        {

            string query = @"INSERT INTO " + Handler_ID + " (STATUS, TESTER, Device, Comments, Who, DATE_TIME, BIG_DATE_TIME, set_temp, actual_temp, set_timer_1, set_timer_2, voltage_set, chamber_condition, wafer_size,"+
            " sampling_qty, bi_aging_board_name, device_qty_per_loading, item_isolation, handler_model, aging_tray_no, ps_1, ps_2, ps_3, clk_1, clk_2, clk_3, clk_4, clk_5, clk_6, clk_7, clk_8, no_of_running_slot, max_no_of_slot, burn_in_board_cleaning) VALUES ('" + stsDes + 
                "','" + machineID + "','" + prodName + "','" + remarks + "','" + user + "','" + date1 + "','" + date2 + "','" + set_temp + "','" + actual_temp + "','" + set_timer_1 + "','" + set_timer_2 +
                "', '"+ voltage_set +"', '"+ chamber_condition +"', '"+ wafer_size +"', '"+ sampling_qty +"', '"+ bi_aging_board_name +"', '"+ device_qty_per_loading +"', '"+ item_isolation +
                "', '" + handlerModel + "', '" + aging_tray_no + "', '" + ps_1 + "', '" + ps_2 + "', '" + ps_3 + "', '" + clk_1 + "', '" + clk_2 + "', '" + clk_3 +
                "', '"+ clk_4 +"', '"+ clk_5 +"', '"+ clk_6 +"', '"+ clk_7 +"', '"+ clk_8 +"' ,'" + no_of_running_slot + "', '" + max_no_of_slot + "', '" + burn_in_board_cleaning + "')";

            Boolean results = Connection.ExecuteThisQuery(query, "Get User", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> export_allmachines()
        {
            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "SELECT * FROM testers_bi order by TesterPF DESC";

            results = Connection.GetDataAssociateArray(query, "Download all Machiness", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> export_to_excel()
        {

            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            List<IDictionary<string, string>> machinelist = new List<IDictionary<string, string>>();

            string query = "SELECT * FROM testers_bi where TesterPF = 'TNR' order by TesterPF DESC";

            machinelist = Connection.GetDataAssociateArray(query, "Download TNR Machines", Connection.promis_connString);

            string query2 = "";

            int machine_list_length = machinelist.Count;

            for (int i = 0; i < machine_list_length; i++)
            {
                query2 += "SELECT * FROM " + machinelist[i]["TesterID"];

                if (machine_list_length - 1 == i)
                {
                    query2 += "";
                }
                else
                {
                    query2 += " UNION ";
                }
            }

            results = Connection.GetDataAssociateArray(query2, "Download all Machiness", Connection.promis_connString);

            return results;
        }

        public Boolean insert_Data_History(string machineID, string stsDes, string date1, string date2, string user, string remarks,
        string prodName, string lotNo, double uph, string stsOwner, string group, string pkgType, int pbft_min, int pbft_max, string pkgLine, int bin1, int total_ng_units_result,
        string unpicked_device, string waferID, string marking_surface, string failure_mechanism, string bump_leads, string part_specification, string missing, string lsg_repair_type,
        string lsg_sample, int vi5_sample, string swi_mode, string swi, string carrier_tape_lot_no, string cover_tape_lot_no)
        {

            string query = "INSERT INTO " + machineID + " (Status,Date_Time,DateTime,Personnel,Comments,Device,Lot_ID,UPH," +
                "STATUS_OWNER,GROUPS,pkg_type,pbft_min,pbft_max,PROD_LINE,bin1,total_ng_units_result,unpicked_device,waferID,marking_surface," +
                "failure_mechanism,bump_leads,part_specification,missing,lsg_repair_type,lsg_sample,vi5_sample,swi_mode,swi,carrier_tape_lot_no,cover_tape_lot_no) VALUES" +
                "('" + stsDes + "','" + date1 + "','" + date2 + "','" + user + "','" + remarks + "','" + prodName + "','" + lotNo + "'," + uph + ",'" + stsOwner + "','" + group + "','" + pkgType + "', '" + pbft_min + "', '" + pbft_max + "', '" + pkgLine + "', '" + bin1 + "', '" + total_ng_units_result + "', '" + unpicked_device + "', '" + waferID + "', '" + marking_surface + "', '" + failure_mechanism + "', '" + bump_leads + "', '" + part_specification + "', '" + missing + "', '" + lsg_repair_type + "', '" + lsg_sample + "', '" + vi5_sample + "', '" + swi_mode + "', '" + swi + "', '" + carrier_tape_lot_no + "', '" + cover_tape_lot_no + "')";

            Boolean results = Connection.ExecuteThisQuery(query, "Get User", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> check_lot(string machineID, string lotNo)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "SELECT * FROM track_record WHERE lot_name = '" + lotNo + "' AND machine = '" + machineID + "'";

            results = Connection.GetDataArray(query, "Check User", Connection.promis_connString);

            return results;
        }

        public Boolean track_lot(string machineID, string track_in, string track_out, string lotNo, string user)
        {

            string query = "INSERT INTO track_record (date_issued,machine,process,track_in,track_out,lot_name,submitter) VALUES (CURRENT_TIMESTAMP,'" + machineID + "', 'TNR', " + track_in + ", " + track_out + ", '" + lotNo + "', '" + user + "')";

            Boolean results = Connection.ExecuteThisQuery(query, "Insert Track Record", Connection.promis_connString);

            return results;
        }

        public Boolean update_track_lot(string machineID, string track_in, string track_out, string lotNo, string user)
        {

            string query = "UPDATE track_record SET track_in = '" + track_in + "', track_out = '" + track_out + "' WHERE machine = '" + machineID + "' AND lot_name = '" + lotNo + "'";

            Boolean results = Connection.ExecuteThisQuery(query, "Insert Track Record", Connection.promis_connString);

            return results;
        }

        public IDictionary<string, string> pbft_reset(string machineID)
        {
            IDictionary<string, string> results = new Dictionary<string, string>();

            string query = "UPDATE testers_bi SET pbft_min = 0, pbft_max = 0, pbft_reset_cause = 'RESET DUE TO LOT CHANGE' WHERE Tester_ID = '" + machineID + "'";

            results = Connection.GetDataArray(query, "Get Package Type", Connection.promis_connString);

            return results;
        }

        public List<IDictionary<string, string>> get_bi_family(){

            List<IDictionary<string, string>> results = new List<IDictionary<string, string>>();

            string query = "SELECT family FROM bi_family";

            results = Connection.GetDataAssociateArray(query, "BI FAMILY DATA", Connection.promis_connString);

            return results;

        }

    }
}