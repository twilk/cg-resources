/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

var str = readline();
var ans="",sign="",count=0;
var ans2="";
var last=-1;
    for (var i = 0; i < str.length; i++) {
        ans2+= "00000000".substr(str[i].charCodeAt(0).toString(2).length+1) +str[i].charCodeAt(0).toString(2) + "";
    }
    printErr(str+"\n"+ans2);
    for(var j=0;j<=ans2.length;j++){
        if(ans2[j]=="0"){
            if(last=="0"){
                count++;
            }else{
                ans+="0".repeat(count);
                if(ans!=""){ans+=" ";}
                count=1;
                ans+="00 ";
            }      
          }else if(ans2[j]=="1"){
            if(last=="1"){
                count++;
            }else{
                ans+="0".repeat(count);
                if(ans!=""){ans+=" ";}
                count=1;
                ans+="0 ";
            }
            
          }else if(ans2[j]==undefined ){
               ans+="0".repeat(count);
               count=1;
               if(j!=ans2.length-1){
                //   ans+=" ";
               }
          }
        last=ans2[j];
        // printErr(last);
        
      }
  print(ans);
