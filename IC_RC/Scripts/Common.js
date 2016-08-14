
//top open create link 

function gotoCreate(url) {
    debugger;
    var cururl = window.location.href;
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
    window.location.href = url;
}


function confirmDelete(url,reloadUrl)
{
    debugger;
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