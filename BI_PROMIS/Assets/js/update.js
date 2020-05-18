$(document).on("click", "#save", function (e) {
    e.preventDefault();
    Update_Machine();
});

$(document).on("click", "#track_in", function (e) {
    e.preventDefault();
    var track = "TRACK IN";
    var carrier_tape_lot_no = $('#carrier_tape_lot_no').val();
    var cover_tape_lot_no = $('#cover_tape_lot_no').val();
    var lotNo = $("#lotNo").val();
    if (carrier_tape_lot_no == "") {
        alert('PLEASE INPUT CARRIR TAPE DESCRIPTION!');
    }
    if (cover_tape_lot_no == "") {
        alert('PLEASE INPUT COVER TAPE LOT#!');
    }
    else {
        $.post(
            base_url + "promis/check_track_by_lot",
            {
                "lotNo": lotNo
            },
            function (lot) {
                if (lot.track_in == 1 && lot.track_out == 1) {
                    alert("LOT ID ALREADY TRACKED OUT. CANNOT BE TRACKED IN AGAIN!");
                }
                else {
                    Update_Machine(track);
                }

            });
    }


});

$(document).on("click", "#track_out", function (e) {
    e.preventDefault();
    var track = "TRACK OUT";
    var carrier_tape_lot_no = $('#carrier_tape_lot_no').val();
    var cover_tape_lot_no = $('#cover_tape_lot_no').val();
    var prev_carrier_tape_lot_no = $('#carrier_tape_lot_no').attr("placeholder");
    var prev_cover_tape_lot_no = $('#cover_tape_lot_no').attr("placeholder");
    if (carrier_tape_lot_no == "" || cover_tape_lot_no == "") {
        if (carrier_tape_lot_no == "" && cover_tape_lot_no != "") {
            alert('PLEASE INPUT CARRIER TAPE DESCRIPTION!');
        }
        else if (cover_tape_lot_no == "" && carrier_tape_lot_no != "") {
            alert('PLEASE INPUT COVER TAPE LOT#!');
        }
        else {
            alert('PLEASE INPUT CARRIR TAPE DESCRIPTION/COVER TAPE LOT#!');
        }
    }
    else {
        if (prev_carrier_tape_lot_no != carrier_tape_lot_no) {

            //alert("\tCARRIER TAPE \t\nOUT: " + carrier_tape_lot_no + " vs IN: " + prev_carrier_tape_lot_no + " \n\tDOES NOT MATCH");

            $('.alertModalMessage').html("CARRIER TAPE<br> OUT: " + carrier_tape_lot_no + " vs IN: " + prev_carrier_tape_lot_no + "<br>DOES NOT MATCH");
            $('#alertModal').modal('show');
            $('.alertModal').click(function () {
                $('#alertModal').modal('hide');
            });
        }
        else if (prev_cover_tape_lot_no != cover_tape_lot_no) {
            //alert("\tCOVER TAPE \t\nOUT: "+ cover_tape_lot_no + " vs  IN: " + prev_cover_tape_lot_no + " \n\tDOES NOT MATCH");

            $('.alertModalMessage').html("COVER TAPE<br> OUT: " + cover_tape_lot_no + " vs IN: " + prev_cover_tape_lot_no + "<br>DOES NOT MATCH");
            $('#alertModal').modal('show');
            $('.alertModal').click(function () {
                $('#alertModal').modal('hide');
            });
        }
        else {
            Update_Machine(track);
        }
    }
});

function Update_Machine(track) {
    var dateTime = new Date($.now());
    var date = Date.parse(dateTime);
    var date1 = date.toString('dd-MMM-yyyy HH:mm:ss');
    var date2 = date.toString('yyyyMMddHHmmss');
    var date3 = date.toString('M/d/yyyy HH:mm');
    //var date3 = date.toString('dd-MMM-yyyy HH:mm');
    var machineID = $("#machineID").val();
    var machinePF = $("#machinePlatform").val();
    var stsOwner = $("option:selected", "#stsOwner").text(); //$(this).attr("value#stsOwner");
    var stsDes = $("option:selected", "#stsDes").text();
    var prodName = $("option:selected", "#prodName").text();
    var pkgLine_temp = $("option:selected", "#pkgLine").text();
    var pkgLine = pkgLine_temp.trim();
    var lotNo = $("#lotNo").val();
    var waferID = $("#waferID").val();
    var uph = $("option:selected", "#uph_select").text();
    var pkgType = $("option:selected", "#pkgType").text();
    var set_temp = $("option:selected", "#set_temp").text();
    var actual_temp = $("option:selected", "#actual_temp").text();
    var set_timer_1 = $("option:selected", "#set_timer_1").val();
    var set_timer_2 = $("option:selected", "#set_timer_2").val();
    var voltage_set = $("option:selected", "#voltage_set").text();
    var chamber_condition = $("option:selected", "#chamber_condition").text();
    var wafer_size = $("#wafer_size").val();
    var sampling_qty = $("#sampling_qty").val();
    var bi_aging_board_name = $("#bi_aging_board_name").val();
    var item_isolation = $("#item_isolation").val();
    var aging_tray_no = $("#aging_tray_no").val();
    var ps_1 = $("#ps_1").val();
    var ps_2 = $("#ps_2").val();
    var ps_3 = $("#ps_3").val();
    var clk_1 = $("#clk_1").val();
    var clk_2 = $("#clk_2").val();
    var clk_3 = $("#clk_3").val();
    var clk_4 = $("#clk_4").val();
    var clk_5 = $("#clk_5").val();
    var clk_6 = $("#clk_6").val();
    var clk_7 = $("#clk_7").val();
    var clk_8 = $("#clk_8").val();
    var no_of_running_slot = $("#no_of_running_slot").val();
    var max_no_of_slot = $("#max_no_of_slot").val();
    var burn_in_board_cleaning = $("#burn_in_board_cleaning").val();
    var device_qty_per_loading = $("#device_qty_per_loading").val();
    var group = $("option:selected", "#group").text();
    var user = $("option:selected", "#user").text();
    var temp_remarks = $("#remarks").val();
    var remarks = temp_remarks.trim();
    var pass = $("#pass").val();
    var handlerModel = $("#handlerModel").val();
    var Handler_ID = $("#handlerID").val();
    //var material_id = $(".inputTxt").val();

    //var track = $(".track").val();
    if (track == "TRACK IN") {
        var track_in = 1;
        var track_out = 0;
    }
    else if (track == "TRACK OUT") {
        var track_in = 1;
        var track_out = 1;
    }

    var BI_DATA_UPDATE = {
        "machineID": machineID,
        "machinePF": machinePF,
        "stsOwner": stsOwner,
        "stsDes": stsDes,
        "prodName": prodName,
        "lotNo": lotNo,
        "uph": uph,
        "pkgType": pkgType,
        "remarks": remarks,
        "group": group,
        "user": user,
        "date1": date1,
        "date2": date2,
        "date3": date3,
        "waferID": waferID,
        "pkgLine": pkgLine,
        "set_temp": set_temp.trim(),
        "actual_temp": actual_temp.trim(),
        "set_timer_1": set_timer_1.trim(),
        "set_timer_2": set_timer_2.trim(),
        "voltage_set": voltage_set.trim(),
        "chamber_condition": chamber_condition.trim(),
        "wafer_size": wafer_size.trim(),
        "sampling_qty": sampling_qty.trim(),
        "bi_aging_board_name": bi_aging_board_name.trim(),
        "device_qty_per_loading": device_qty_per_loading.trim(),
        "item_isolation": item_isolation.trim(),
        "aging_tray_no": aging_tray_no.trim(),
        "ps_1": ps_1.trim(),
        "ps_2": ps_2.trim(),
        "ps_3": ps_3.trim(),
        "clk_1": clk_1.trim(),
        "clk_2": clk_2.trim(),
        "clk_3": clk_3.trim(),
        "clk_4": clk_4.trim(),
        "clk_5": clk_5.trim(),
        "clk_6": clk_6.trim(),
        "clk_7": clk_7.trim(),
        "clk_8": clk_8.trim(),
        "no_of_running_slot": no_of_running_slot.trim(),
        "max_no_of_slot": max_no_of_slot.trim(),
        "burn_in_board_cleaning": burn_in_board_cleaning.trim(),
        "handlerModel": handlerModel.trim(),
        "Handler_ID": Handler_ID.trim(),
        "track_in": track_in,
        "track_out": track_out
    }
    console.log(BI_DATA_UPDATE);

    $.post(
       base_url + "promis/hash_test",
       {
           "pass": pass
       },
       function (password) {
           $.post(
                base_url + "promis/check_user",
                {
                    "user": user,
                    "pass": password
                },
                function (access) {

                    if (access.email_address != null) {

                        $.post(
                            base_url + "promis/update_machine",
                            BI_DATA_UPDATE,
                            function (data) {
                                if (track == "TRACK IN" || track == "TRACK OUT") {
                                    $.post(
                                         base_url + "promis/track_lot",
                                         BI_DATA_UPDATE,
                                         function (track) {
                                             console.log('trackable');
                                             window.location.reload();
                                         }
                                     )
                                }
                                else {
                                    console.log('no track');
                                    window.location.reload();
                                }
                            }
                        );
                    }
                    else {
                        alert('Invalid Credentials, Please Try Again');
                    }
                }
            )
       }
    )
}