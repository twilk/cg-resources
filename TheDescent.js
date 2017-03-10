
var mountainH=Array(),maxid=0;
while (true) {
    for (var i = 0; i < 8; i++) {
        mountainH[i]=parseInt(readline()); // represents the height of one mountain.
    }
    
    for (var i = 0; i < 8; i++) {
        if(mountainH[maxid]<mountainH[i]){
           maxid=i; 
    
        }
        printErr("i:"+i+" "+mountainH+" "+maxid);        
    }
    
    mountainH[maxid]==-1
    print(maxid);
}
