
//top open create link 

function gotoCreate(url) {
    debugger;
    var cururl = window.location.href;
    var isAdmin = readCookie('AdminCookie');
    if (isAdmin.toUpperCase() != "ADMIN")
    {
        if (url.indexOf('CertifiedPersons') > -1)
        {
            if (url.indexOf('edit') > -1)
            {
                
            }
            else
            {
                alert('admin privilages required.');
                return;
            }
            
        }
        else
        {
            alert('admin privilages required.');
            return;
        }

     }
    var comidx = cururl.split('/');
    var returnUrl=""; 
    for(i=3;i<comidx.length;i++)
    {
        if(comidx[i]!="#")
        {
            returnUrl+="/"+comidx[i];
        }
        
    }
    var newurl="";
    if (url.indexOf('?') > 0)
    {
        newurl = url + "&returnUrl=" + returnUrl;;
    }
    else
    {
        newurl = url + "?returnUrl=" + returnUrl;
    }
     
    window.location.href = newurl;
}

function gotoPage(url)
{
    debugger;
    window.location.href = url;
}


function confirmDelete(url,reloadUrl)
{
    debugger;
    var isAdmin = readCookie('AdminCookie');
    if (isAdmin.toUpperCase() != "ADMIN") {

        alert('admin privilages required.');
        return;
    }
    if(confirm('you are about to delete this record are you sure?'))
    {
        $.ajax({
            url: url,
            type: "POST",
            success: function (data) {
                if(data)
                {
                    $('#divContainer').load(reloadUrl+'/GetData');
                }
            },
            error: function (err) {
                alert(err.statusText);
            }


        });
    }
}


function showOverLay() {
    $('#overlay').css('display', 'block');

}

function hideOverLay() {
    $('#overlay').css('display', 'none');

}

$(document).on('click', '#closeDialog1', function () {
    $('#overlayDialog').css('display', 'none');
});


function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

$(document).ready(function () {
    

        //$("#myModal iframe").attr({
        //    'src': src,
        //    'height': height,
        //    'width': width
        //});
    //});


    $("#person").autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: '/CertifiedPersons/GetPersons',
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term,
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        debugger;
                        return { label: item.FullName, value: item.FullName, id: item.ID };
                    }));
                }
            });
        },
        select: function (event, ui) {
            $("#PersonID").val(ui.item.id);
        }
    });
});

//$("#person").autocomplete({
//    source: 
//});