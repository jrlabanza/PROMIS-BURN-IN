function checking(lotNo) {

    

}

$(document).on("click", "#biID", function () {
    
    var DataId = $(this).attr("data-machine-id");
    $('#myModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: true
    })
    showData(DataId);

});

function showData(DataId) {
    
    $.post( //on ready
        base_url + "promis/show_data",
        {
            "id": DataId
        },
        function (data) {

            $.post(
                base_url + "promis/check_track_by_machine",
                {
                    "machineID": data.Tester_ID,
                },
                function (track_by_machine) {
                    $.post(

                    base_url + "promis/check_track_by_lot",
                    {
                        "lotNo": data.Lot_Name,
                    },
                    function (track_by_lot) {

                        var status_owner_track = $("#status_owner_track").val();
                        var status_track = $("#status_track").val();
                        var machine = $("#machineID").val();
  
                        //track data
                        if ((track_by_machine.track_in == 1 && track_by_machine.track_out == 0) || ((track_by_machine.track_in == 1 && track_by_machine.track_out == 0) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION"))) { // track check using machine ID
                            //alert('machine still tracked in');
                            $('#track_in').hide();
                            $("#lotNo").prop('disabled', true);
                            if (track_by_lot.machine == machine) {
                                $('#track_out').show();
                                $('#track_in').hide();
                            }
                            else {
                                $('#track_out').hide();
                            }

                            $('.track').attr('value', '');

                        }
                        else if ((track_by_lot.track_in == 1 && track_by_lot.track_out == 0) || ((track_by_lot.track_in == 1 && track_by_lot.track_out == 0) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION"))) {
                            //alert('lot still tracked in');
                            $('#track_in').hide();
                            if (track_by_lot.machine == machine) {
                                $('#track_out').show();
                                $('#track_in').hide();
                            }
                            else {
                                $('#track_out').hide();
                            }
                            $('.track').attr('value', '');
                        }

                        else if (((track_by_machine.track_in == 1 && track_by_machine.track_out == 1) || (track_by_machine.track_in == null && track_by_machine.track_out == null)) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION")) {
                            //alert('can track in by machine');
                            $('#track_in').show();
                            $('#track_out').hide();
                            $('.track').attr('value', '');
                        }

                        else if (((track_by_lot.track_in == 1 && track_by_lot.track_out == 1) || (track_by_lot.track_in == null && track_by_lot.track_out == null)) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION")) {
                            //alert('can track in by lot id');
                            $('#track_in').show();
                            $('#track_out').hide();
                            $('.track').attr('value', '');
                        }
                        else {
                            //alert("track available")
                            $('#track_in').hide();
                            $('#track_out').hide();
                        }
                    });
                });

                $("#date").val(data.File_DateTime);
                $("#machineID").val(data.Tester_ID);
                $("#machineIDName").html(data.Tester_ID);
                $("#carrier_tape_lot_no").val(data.carrier_tape_lot_no);
                $('#status_track').val(data.Status_Desc);
                $('#status_owner_track').val(data.Status_Owner);
                $("#lotNo").val(data.Lot_Name); // lot no
                $("#waferID").val(data.waferID);
                $("#pkgType").append("<option class='pkgType' value='" + data.pkg_type + "'>" + data.pkg_type + "</option>");
                $("#pkgLine").append("<option class='pkgLine' value = " + data.Prod_Line + "> " + data.Prod_Line + "</option>");
                $("#set_temp").val(data.set_temp);
                //$("#set_timer_1").append("<option selected value = " + data.set_timer_1 + "> " + data.set_timer_1 + "</option>");
                //$("#set_timer_2").append("<option selected value = " + data.set_timer_2 + "> " + data.set_timer_2 + "</option>");

                $("#set_timer_1").val(data.set_timer_1);
                $("#set_timer_2").val(data.set_timer_2);

                $("#voltage_set").val(data.voltage_set);
                $("#chamber_condition").val(data.chamber_condition);
                $("#handlerModel").val(data.handler_model);
                $("#handlerID").append("<option selected value = " + data.Handler_ID + "> " + data.Handler_ID + "</option>");
                $("#item_isolation").val("");
                $("#aging_tray_no").val("");
                $("#ps_1").val(data.ps_1);
                $("#ps_2").val(data.ps_2);
                $("#ps_3").val(data.ps_3);
                $("#clk_1").val(data.clk_1);
                $("#clk_2").val(data.clk_2);
                $("#clk_3").val(data.clk_3);
                $("#clk_4").val(data.clk_4);
                $("#clk_5").val(data.clk_5);
                $("#clk_6").val(data.clk_6);
                $("#clk_7").val(data.clk_7);
                $("#clk_8").val(data.clk_8);
                $("#no_of_running_slot").val("");
                $("#max_no_of_slot").val("");
                $("#burn_in_board_cleaning").val("");
                $("#remarks").val("");

                
                //$("#wafer_size").val(data.wafer_size);
                //$("#sampling_qty").val(data.sampling_qty);
                //$("#bi_aging_board_name").val(data.bi_aging_board_name);
                //$("#device_qty_per_loading").val(data.waferID);

                $.post(
                    base_url + "promis/get_prod_line",
                    function (data) {

                        var prod_line = '';
                        
                        $.each(data, function (i, prdl) {
                            if (data.ProdLine == $("#pkgLine").val()) {

                            }
                            else {
                                prod_line += "<option class = 'pkgLine' value = " + prdl.ProdLine + "> " + prdl.ProdLine + "</option>"
                            }
                        });
                        $("#pkgLine").append(prod_line);
                    }
                );

                $.post(
                    base_url + "promis/get_handler_mode",
                    {
                        "handlerID": data.Handler_ID
                    },
                    function (data) {

                        $('#machinePlatform').val(data.equipt_model);
                        //alert(data.equipt_model);
                        $.post(
                            base_url + "promis/get_uph",
                            {
                                "machinePF": data.equipt_model,
                                "family": data.Device,

                            },
                            function (data) {
                                if (data.uph == null) {

                                    $("#uph_select").append("<option class='uph' value='0'>0</option>");

                                }
                                else {

                                    $("#uph_select").append("<option class='uph' value='" + data.uph + "'>" + data.uph + "</option>");

                                }
                            }
                        );
                    }
                );

                $.post(
                    base_url + "promis/get_status_owner",
                    function (stsO) {
                        var StatusOwner = ''; //status owner

                        StatusOwner += "<option class= 'stat_Owner' value='" + data.Status_Owner + "'>" + data.Status_Owner + "</option>";

                        $.each(stsO, function (i, StsOwner) {

                            if (StsOwner.owner == data.Status_Owner)//if the array data found a match it will only leave a black
                            {
                            }
                            else {
                                StatusOwner += "<option class= 'stat_Owner' value='" + StsOwner.owner + "'>" + StsOwner.owner + "</option>";
                            }

                        });
                        $("#stsOwner").append(StatusOwner);
                    }
                );
            
                $.post(
                    base_url + "promis/get_status",
                    {
                        "stsOwner": data.Status_Owner
                    },
                    function (stsOwner) {
                        var stat = ''; //status
                        stat += "<option class='status' value='" + data.Status_Desc + "'>" + data.Status_Desc + "</option>";

                        $.each(stsOwner, function (i, desc) {

                            if (desc.description == data.Status_Desc) {
                            }
                            else {
                                stat += "<option class='status' value='" + desc.description + "'>" + desc.description + "</option>";
                            }

                        });

                        $("#stsDes").append(stat);

                    }

                );

                //$.post(
                //    base_url + "promis/get_device",

                //    function (device) {

                //        var show_device = ''; //family
                //        show_device += "<option class='fam' value='" + data.Device + "'>" + data.Device + "</option>";

                //        $.each(device, function (i, family) {

                //            if (family.family == data.Device) {
                //            }
                //            else {
                //                show_device += "<option class='family' value='" + family.family + "'>" + family.family + "</option>";
                //            }

                //        });
                //        $("#prodName").append(show_device);
                //    }
                //)

                $.post(
                      base_url + "promis/get_bi_family",

                      function (device) {

                          var show_device = ''; //family
                          show_device += "<option class='fam' value='" + data.Device + "'>" + data.Device + "</option>";

                          $.each(device, function (i, family) {

                              if (family.family == data.Device) {
                              }
                              else {
                                  show_device += "<option class='family' value='" + family.family + "'>" + family.family + "</option>";
                              }

                          });
                          $("#prodName").append(show_device);
                      }
                  )
                
                $.post(
                    base_url + "promis/get_user",
                    function (usr) {

                        var users = '';
                        $.each(usr, function (i, username) {
                            users += "<option class='user' value='" + username.email_address + "'>" + username.email_address + "</option>"
                        });

                        $("#user").append(users);

                    }
                );

                $.post(
                    base_url + "promis/get_handler",
                    {
                        //"handlerModel": data.
                    },
                    function (handler) {
                        console.log(handler);
                        var handler_append = "";

                        $.each(handler, function (i, handler_list) {

                            handler_append += "<option value='" + handler_list.Equipt_ID + "'>" + handler_list.Equipt_ID + "</option>";

                        });

                        $("#handlerID").append(handler_append);

                    }
                );
            }
        );
    }

//status owner
$(document).on("change", "#stsOwner", function () {

    var stsOwner = $("#stsOwner").val();
    var machineID = $("#machineID").val();
    var lotNo = $("#lotNo").val();

    $('option').remove('.status');

    $.post(
        base_url + "promis/get_status",
        {
            "stsOwner": stsOwner
        },
        function (data)
        {

            var StatusDesc = '';
            //stsDes
            StatusDesc += "<option class='status' value=''></option>";
            $.each(data, function (i, status) {
                StatusDesc += "<option class='status' value='" + status.description + "'>" + status.description + "</option>";
            });
            
            $("#stsDes").append(StatusDesc);

        }

    );
    
    $.post(
    base_url + "promis/check_track_by_machine",
    {
        "machineID": machineID,
    },
    function (track_by_machine) {
        console.log(track_by_machine);
        $.post(

        base_url + "promis/check_track_by_lot",
        {
            "lotNo": lotNo,
        },
        function (track_by_lot) {
            console.log(track_by_lot);
            var status_owner_track = $("#status_owner_track").val();
            var status_track = $("#status_track").val();
            var machine = $("#machineID").val();

            //track data
            if ((track_by_machine.track_in == 1 && track_by_machine.track_out == 0) || ((track_by_machine.track_in == 1 && track_by_machine.track_out == 0) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION"))) { // track check using machine ID
                //alert('machine still tracked in');
                $('#track_in').hide();
                if (track_by_lot.machine == machine) {
                    $('#track_out').show();
                    $('#track_in').hide();
                }
                else {
                    $('#track_out').hide();
                }

                $('.track').attr('value', '');

            }
            else if ((track_by_lot.track_in == 1 && track_by_lot.track_out == 0) || ((track_by_lot.track_in == 1 && track_by_lot.track_out == 0) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION"))) {
                //alert('lot still tracked in');
                $('#track_in').hide();
                if (track_by_lot.machine == machine) {
                    $('#track_out').show();
                    $('#track_in').hide();
                }
                else {
                    $('#track_out').hide();
                }
                $('.track').attr('value', '');
            }

            else if (((track_by_machine.track_in == 1 && track_by_machine.track_out == 1) || (track_by_machine.track_in == null && track_by_machine.track_out == null)) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION")) {
                //alert('can track in by machine');
                $('#track_in').show();
                $('#track_out').hide();
                $('.track').attr('value', '');
            }

            else if (((track_by_lot.track_in == 1 && track_by_lot.track_out == 1) || (track_by_lot.track_in == null && track_by_lot.track_out == null)) && (status_owner_track == "PRODUCTION" || $("#stsOwner").val() == "PRODUCTION")) {
                //alert('can track in by lot id');
                $('#track_in').show();
                $('#track_out').hide();
                $('.track').attr('value', '');
            }
            else {
                //alert("track available")
                //$('#track_in').hide();
                //$('#track_out').hide();
            }

            //Dynamic Filter
            if (status_track == "LSG REPAIR") {// LSG repair release to ptroduction

                if ($("#stsOwner").val() == "PRODUCTION") {
                    $(".package_line").hide();
                    //$(".status_owner").hide();
                    //$(".status").hide();
                    $(".lot_no").hide();
                    $(".family_name").hide();
                    $(".wafer_id").hide();
                    $(".good_units").hide();
                    $(".total_ng_units_results").hide();
                    $(".unpicked_device").hide();
                    $(".marking_surface").hide();
                    $(".bump_leads").hide();
                    $(".missing").hide();
                    $(".lsg_sample").hide();
                    $(".vi5_sample").hide();

                }
            }

            if (status_owner_track == "SETUP") // setup relase to production
            {
                if ($("#stsOwner").val() == "PRODUCTION") {
                    $(".good_units").hide();
                    $(".total_ng_units_results").hide();
                    $(".unpicked_device").hide();
                    $(".marking_surface").hide();
                    $(".bump_leads").hide();
                    $(".missing").hide();
                    $(".lsg_sample").hide();
                    $(".vi5_sample").hide();

                }
            }

            if (status_owner_track == "PRODUCTION") { //Production to setup-lsg-wait-handler-pm-wait
                if ($("#stsDes").val() == "LSG WAIT" || $("#stsDes").val() == "HANDLER PM" || $("#stsDes").val() == "HANDLER WAIT" || $("#stsOwner").val() == "SETUP") {
                    $(".lot_no").hide();
                    $(".family_name").hide();
                    $(".wafer_id").hide();
                    $(".failure_mechanism").hide();
                    $(".part_specification").hide();
                    $(".lsg_repair_type").hide();
                    $(".package_type").hide();
                }
            }

            if (status_track == "HANDLER PM") // handler pm release to production
            {

                if ($("#stsOwner").val() == "PRODUCTION") {
                    $(".package_line").hide();
                    //$(".status_owner").hide();
                    //$(".status").hide();
                    $(".lot_no").hide();
                    $(".family_name").hide();
                    $(".wafer_id").hide();
                    $(".good_units").hide();
                    $(".total_ng_units_results").hide();
                    $(".unpicked_device").hide();
                    $(".marking_surface").hide();
                    $(".bump_leads").hide();
                    $(".missing").hide();
                    $(".lsg_sample").hide();
                    $(".vi5_sample").hide();

                }
            }
            //final check
            if ($("#stsOwner").val() != "PRODUCTION") {
                $('#track_in').hide();
                $('#track_out').hide();
            }
        });
    });

    
});

$(document).on("change", "#prodName", function () {

    $('option').remove('.pkgType');
    $('option').remove('.uph');
    var id = $(this).val();
    var machineID = $("#machineID").val();
    var machinePlatform = $('#machinePlatform').val();
    $('option').remove('.stat');
   
       $.post(
            base_url + "promis/get_package_data",
            {
                "id": id
            },
            function (data) {
                display = "<option class='pkgType' value='" + data.Pkg_Type + "'>" + data.Pkg_Type + "</option>"
                $("#pkgType").append(display);
                //stsDes
            }
        );

       //$.post(
       //    base_url + "promis/get_uph_check",
       //    {
       //        "handlerPF": machinePlatform,
       //        "family": id
       //    },
       //    function (data) {
       //        $("#uph_select").append("<option class='uph' value='" + data.uph + "'>" + data.uph + "</option>;");
       //    }
       //);
       $.post(
            base_url + "promis/get_uph",
            {
                "machinePF": machinePlatform,
                "family": id,

            },
            function (data) {
                if (data.uph == null) {
                    $("#uph_select").append("<option class='uph' value='0'>0</option>;");
                }
                else {
                    $("#uph_select").append("<option class='uph' value='" + data.uph + "'>" + data.uph + "</option>;");
                }

                //
            }

        );
});

$(document).on("click", "button.close", function () {
    $('option').remove('.fam');
    $('option').remove('.stat_Owner');
    $('option').remove('.status');
    $('option').remove('.pkgType');
    $('option').remove('.uph_select');
    $('#lotNo').prop('disabled', false);
    $('.track').attr('value', '');
    $('option').remove('.pkgLine');
    $('option').remove('.uph');
    $('#lotNo').attr('readonly', false);

    $('option').remove('#set_temp');
});



$(document).on("change", "#lotNo", function ()
{
    if ($("#lotNo").val() == "" || $("#lotNo").val() == "N/A") {
        $('#track_in').prop('disabled', true);
    }

});

$(document).on("click", "#track_in", function ()
{
    
    if ($("#lotNo").val() == "" || $("#lotNo").val() == "N/A") {
        alert('Please Input LOT NO.');
    }
    else
    {
        $('input.track').val('TRACK IN');
        $('#track_in').attr('disabled', true);
        //alert($('input.track').val());
        $('.failure_mechanism').hide();
        $('.part_specification').hide();
        $('.lsg_repair_type').hide();
        $(".good_units").hide();
        $(".total_ng_units_results").hide();
        $(".unpicked_device").hide();
        $(".marking_surface").hide();
        $(".bump_leads").hide();
        $(".missing").hide();
        $(".lsg_sample").hide();
        $(".vi5_sample").hide();
        
    }
});

$(document).on("click", "#track_out", function () {
    
    $('input.track').val('TRACK OUT');
    $('#track_out').attr('disabled', true);
    //alert($('input.track').val());
    $(".failure_mechanism").hide();
    $(".part_specification").hide();
    $(".lsg_repair_type").hide();
    $(".package_line").hide();
    $(".status_owner").hide();
    $(".status").hide();
    $(".lot_no").hide();
    $(".family_name").hide();
    $(".wafer_id").hide();
});



