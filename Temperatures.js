var n = parseInt(readline());
var temps = readline().split(' ');
var pos = Array(),neg = Array();
for(i=0;i<n;i++){
    ti=temps[i];
    if(ti>=0){
        pos.push(ti);
    }else{
        neg.push(ti);
    }
}  

a = Math.min.apply(null, pos);
b = Math.max.apply(null, neg);
         
if(typeof pos[0]=="undefined" && typeof neg[0]=="undefined"){
    print("0");
}else if(typeof pos[0]=="undefined"){
    print(b);
}else if(typeof neg[0]=="undefined"){
    print(a);
}else if(pos[0]===0){
        print("0");
}else if(Math.abs.apply(a)>Math.abs.apply(b)){
    print(b);
}else{
    print(a);
}
