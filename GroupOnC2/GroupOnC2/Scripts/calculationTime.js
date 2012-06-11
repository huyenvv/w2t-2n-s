
        // TESTING: comment out the line below to print out the "dateFuture" for testing purposes
        //document.write(dateFuture +"<br />");
        
        //###################################
        //nothing beyond this point
        function GetCount() {
            //dateNow = new Date();             					//grab current date
            var amount = dateFuture.getTime() - dateNow.getTime();
          //  alert(dateFuture.getYear());
            //re calculate date (add 1 seconds to this timespan)
            dateNow.setTime(dateNow.getTime() + 1000);
            
            // time is already past
            if (amount < 0) {
                {
                    if (document.getElementById("countbox") != null) {
                        document.getElementById('countbox').innerHTML = "00 : 00 : 00";
                        clearTimeout(t);
                    }
                }
                /*     var url = '(Html.MakeRout("DealDetails", new { cityAlias = CurrentDealContext.Context().CityAlias, dealAlias = CurrentDealContext.Context().Alias }))';
                
                window.location.href = url;*/
            }
            // date is still good
            else {
                var days = 0; var hours = 0; var mins = 0; var secs = 0; var out = "";

                amount = Math.floor(amount / 1000); //kill the "milliseconds" so just secs

                days = Math.floor(amount / 86400); //days
                
                amount = amount % 86400;

//                if (days < 4) {
//                    days = '0' + days;
//                }

                hours = Math.floor(amount / 3600); //hours
                amount = amount % 3600;

//                if (hours < 10) {
//                    hours = '0' + hours;
//                }

                mins = Math.floor(amount / 60); //minutes
                amount = amount % 60;

                if (mins < 10) {
                    mins = '0' + mins;
                }

                secs = Math.floor(amount); //seconds

                if (secs < 10) {
                    secs = '0' + secs;
                }
                
                if ((days * 24) + hours > 72)
                {
//                    if ((days * 24) + hours <= 240)
//                    {
//                        out = '0' + days + " ngày";
//                    }
//                    else
//                    {
                        out = days + " ngày";
//                    }
                }
                else
                {
                    var totalHour = (days * 24) + hours;

                    if (totalHour < 10)
                    {
                        totalHour = '0' + totalHour;
                    }
                    out = totalHour + " : " + mins + " : " + secs;                
                }

                if($('#countbox').length>0) {
                    $('#countbox').html(out);
                }  
                //document.getElementById('countbox').innerHTML = out;
               
                var t = setTimeout("GetCount()", 1000);
            }
        }
        
        