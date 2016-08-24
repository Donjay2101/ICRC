
$(document).on('click', '#completeView', function () {
    showOverLay();
                $('#divContainer').load('/TestScores/GetData', function () {
                    hideOverLay();
                });
     });

$(document).on('click', '#normalView', function () {
    showOverLay();
    $('#divContainer').load('/TestScores/NormalView', function () {
        hideOverLay();
    });
});



//lazy loading code
$(document).on('focus', '#txtLastName', function () {
   
    showOverLay();
    GetLastNames();                   
});
       


function GetLastNames()
{
    var val = $('#pagenum').val();
   
    $.ajax({
        url: "/TestScores/GetLastNames?PageNum=" + val,
        type: "GET",
        success: function (data) {
            hideOverLay();
            $('#cmbLastName').css('display', 'block');
            var htmlString = "";
            for (i = 0; i < data.length; i++) {
                htmlString += "<li>" + data[i].LastName + "</li>";
            }
            $('#cmbLastName').append(htmlString);
            var intval = parseInt(val);
            debugger;
            intval = intval+1;
            //console.log(intval);
            $('#pagenum').val(intval);

        }

    });
}

$(document).ready(function () {

    $('#cmbLastName').scroll(function () {
        if ($(this)[0].scrollHeight - $(this).scrollTop() <= $(this).outerHeight()) {

            // alert("end of scroll");
            // You can perform as you want
            var data=$('#txtLastName').val();

            //if (data == "" && data == undefined)
            //{
            GetLastNames();
            //}
                    
        }
    });


    $(document).on('click','#cmbLastName>li',function (e) {
       
        var data = $(this).html();
        $('#txtLastName').val(data);
        // console.log(data);
        console.log($('#txtLastName').val());
        $('#cmbLastName').css('display', 'none');
    });
   
            

           

           
    $(document).on('mouseleave', '#parentDiv', function () {
        $('#LastName').focus();
        $('#cmbLastName').css('display', 'none');
        $('#tblfirstname').css("display",'none');
    });

    //$('#txtLastName').change(function () {
    //    console.log('asdadasd');
    //    var data = $(this).val();
    //    if (data.length > 3) {
    //        $.ajax({
    //            url: "/TestScore/SearchLastNames?initial=" + data,
    //            type: "GET",
    //            success: function (data) {
    //                var htmlString = "";
    //                for (i = 0; i < data.length; i++) {
    //                    htmlString += "<li>" + data[i].LastName + "</li>";
    //                }
    //                $('#cmbLastName').html(htmlString);
    //            },

    //        });
    //    }

    //});
    //$(document).on('focusout', '#txtLastName', function () {
    //    //$('#LastName').focus();
    //    $('#cmbLastName').css('display', 'none');
    //});

});
        

        

////lazy loading code
      

//AutoComplete Code

$(document).on('input', '#txtLastName', function () {
    debugger;
    var data = $(this).val();
    if (data.length >=3) {
        $.ajax({
            url: "/TestScores/SearchLastNames?initial=" + data,
            type: "GET",
            success: function (data) {
                var htmlString = "";
                for (i = 0; i < data.length; i++) {
                    htmlString += "<li>" + data[i].LastName + "</li>";
                }
                $('#cmbLastName').html(htmlString);
                $('#tblfirstname').html("");
            },

        });
    }
    else
    {
      //  GetLastNames();
    }


});


function getFirstName() {
    showOverLay();
    var data = $('#txtLastName').val();
    $.ajax({
        url: "/TestScores/GetFirstNames?name=" + data,
        data: "GET",
        success: function (data) {
            hideOverLay();
            if (data != null) {
                $('#tblfirstname').html("");
                var htmlString = "<tr>"+
                                            "<th>FirstName</th>"+
                                            "+<th>MI</th>"+
                                            "<th>Address1</th>"+
                                            "<th>City</th>"+
                                            "<th>State</th>"+
                                        "</tr>";
                for (i = 0; i < data.length; i++) {
                    htmlString += "<tr>" +
                            "<td>" + data[i].FirstName + "</td>" +
                            "<td>" + data[i].MiddleName + "</td>" +
                            "<td>" + data[i].Address1 + "</td>" +
                            "<td>" + data[i].City + "</td>" +
                            "<td>" + data[i].State + "</td>" +
                            "</tr>";
                }
                $('#tblfirstname').append(htmlString);
                       
            }
            $('#tblfirstname').css('display', 'block');
        }
    });

}
$(document).on('focus', '#txtFirstName', function () {
    debugger;
    var html=$('#tblfirstname').html();
    
    if (html == undefined || html == "")
    {
        getFirstName();
    }

    $('#tblfirstname').css('display', 'block');
});

$(document).on('click', '#tblfirstname tr', function () {
            
    debugger;
    var FirstName=$(this).find('td').eq(0).html();
    if (FirstName != undefined)
    {
        var LastName = $('#txtLastName').val();
        var MiddleName = $(this).find('td').eq(1).html();
        var Address1= $(this).find('td').eq(2).html();
        var City= $(this).find('td').eq(3).html();
        var State = $(this).find('td').eq(4).html();

        var obj = {};

        obj.FirstName = FirstName;
        obj.LastName = LastName;
        // obj.MiddleName = MiddleName;
        obj.Address1 = Address1;
        //obj.City= City;
        //obj.State = State;
                
        getFullData(obj);


    }
            

});

function getFullData(obj)
{
    showOverLay();
    $('#tblfirstname').css('display', 'none');
    $.ajax({
        url: "/TestScores/GetFullData",
        data: { model: obj },               
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            hideOverLay();
            if (data != null)
            {

                $('#LastName').val(data[0].LastName);
                $('#FirstName').val(data[0].FirstName);
                $('#MiddleName').val(data[0].MiddleName);
                $('#Address1').val(data[0].Address1);
                $('#EmailAddress').val(data[0].EmailAddress);
                $('#City').val(data[0].City);
                $('#State').val(data[0].State);
                $('#ZipCode').val(data[0].ZipCode);
                $('#ZipPlus').val(data[0].ZipPlus);
                var htmlString = "";
                for (i = 0;i<data.length;i++)
                {
                    htmlString+="<tr>"+
                         "<td>" + data[i].Exam+ "</td>" +
                            "<td>" +ConvertJsonDateString(data[i].ExamDate)+ "</td>" +
                            "<td>" + data[i].Status + "</td>" +
                            "<td>" + data[i].Score + "</td>" +
                            "<td>" + data[i].TestingCompanyName + "</td>" +
                            "<td>" + data[i].BoardName + "</td>" +
                            "<td>" +
                "<b>" +

                    "<a href='/CertifiedPersons/edit?id=69336' class='modal_link' id='editlink' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Edit'><span class='glyphicon glyphicon-pencil space' aria-hidden='true'></span></a>" +
                "</b>" +
            "</div></td>" +
			"<td ><div>" +
                                    "<b>" +
                                        "<a href='#' onclick='confirmDelete('/CertifiedPersons/Delete/69336','/CertifiedPersons')' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Delete'><span class='glyphicon glyphicon-trash space' aria-hidden='true'></span></a>" +
                                    "</b>" +
                                "</div></td>" +
                            "</tr>";

                }
                $('#tblResult').append(htmlString);
                        
            }
                    
        },
        error: function (err) {
            console.log(err);
            alert(err.statusCode);
            alert(err.statusText);
            debugger;
        }                
    });
}



function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = monthString + '/' + dayString + '/' + year;
    }
    return shortDate;
};



function showOverLay()
{
    $('#overlay').css('display', 'block');
    
}

function hideOverLay() {
    $('#overlay').css('display', 'none');
    
}