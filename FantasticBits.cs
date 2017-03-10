using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

/**
 * Grab Snaffles and try to throw them through the opponent's goal!
 * Move towards a Snaffle and use your team id to determine where you need to throw it.
 **/
class Player
{   
        //List<Snuffles> snaffles;
        public static int teamModfier;
        public static Snuffles[] snaffles;
        // public static Bludgers[] bludgers;
        public static List<Wizards> wiz;
        public static List<OpponentWizards> oppwiz;
        public static int[] petrified;
        public static int oppMagic,oppMagicLast,flipendoCasted,anyFlipendo;
//     Wizards wiz1= new Wizards();    
//     Wizards wiz2= new Wizards();
//     OpponentWizards oppwiz1= new OpponentWizards();    
//     OpponentWizards oppwiz2= new OpponentWizards();
    
    
    static void Main(string[] args)
    {
        string[] inputs;  
        int myTeamId = int.Parse(Console.ReadLine()); // if 0 you need to score on the right of the map, if 1 you need to score on the left
        SetTeam(myTeamId);
        Initialize();
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int myScore = int.Parse(inputs[0]);
            int myMagic = int.Parse(inputs[1]);
            inputs = Console.ReadLine().Split(' ');
            int opponentScore = int.Parse(inputs[0]);
            int opponentMagic = int.Parse(inputs[1]);
            // scan opponent Magic is POWERFULL!
            if(oppMagic==102){
                oppMagic=opponentMagic;
            }else{
                oppMagicLast=oppMagic;
                oppMagic=opponentMagic;
            }
            int entities = int.Parse(Console.ReadLine()); // number of entities still in game
            Console.Error.WriteLine("Snaffles in game:" + (entities-4-2));
            int snaffleCounter=entities-6;
            for (int i = 0; i < entities; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                
                int entityId = int.Parse(inputs[0]); // entity identifier
                string entityType = inputs[1]; // "WIZARD", "OPPONENT_WIZARD" or "SNAFFLE" (or "BLUDGER" after first league)
                int x = int.Parse(inputs[2]); // position
                int y = int.Parse(inputs[3]); // position
                int vx = int.Parse(inputs[4]); // velocity
                int vy = int.Parse(inputs[5]); // velocity
                int state = int.Parse(inputs[6]); // 1 if the wizard is holding a Snaffle, 0 otherwise
                // vv wstawianie danych do list vv
                if(entityType.Equals("WIZARD") ){
                Console.Error.WriteLine("WizLOG: Wiz[" + entityId + "] x:" + x + " y:" + y + " vx:" + vx + " vy:" + vy + " state:" + state);
                    if(myTeamId==0){
                        if(entityId==0){
                            wiz[0].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            
                            //Wizards wiz1=new Wizards(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go Wiz1 !");
                        } 
                        if(entityId==1){
                            wiz[1].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go Wiz2 !");
                        }
                    }
                    if(myTeamId==1){
                        if(entityId==2){
                            wiz[0].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go Wiz1 ! team1");
                        }
                        if(entityId==3){
                            wiz[1].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go Wiz2 ! team1");
                        }
                    }
                }
                if(entityType.Equals("OPPONENT_WIZARD") ){
                //Console.Error.WriteLine("cokolwiek sie dzieje teamID:"+ myTeamId + " entityId:" + entityId + " entityType:" + entityType + " x:" + x + " y:" + y + " vx:" + vx + " vy:" + vy + " state:" + state);
                Console.Error.WriteLine("OppLOG: oppWiz[" + entityId + "] x:" + x + " y:" + y + " vx:" + vx + " vy:" + vy + " state:" + state);
                    if(myTeamId==0){
                     if(entityId==2){
                            oppwiz[0].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Wizards wiz1=new Wizards(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go oppWiz1 !");
                        }
                        if(entityId==3){
                            oppwiz[1].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go oppWiz2 !");
                        }
                    } 
                    if(myTeamId==1){
                      if(entityId==0){
                            oppwiz[0].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Wizards wiz1=new Wizards(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go oppWiz1 !");
                        } 
                        if(entityId==1){
                            oppwiz[1].UpdateState(entityId,entityType,x,y,vx,vy,state);
                            //Console.Error.WriteLine("Go oppWiz2 !");
                        }
                    }
                }
                if(entityType.Equals("SNAFFLE") ){
                    snaffles[entityId].UpdateState(entityId,entityType,x,y,vx,vy,state);
                }
                if(entityType.Equals("BLUDGER") ){
                    snaffles[entityId].UpdateState(entityId,entityType,x,y,vx,vy,state);
                }
                
                // ^^ wstawianie danych do list ^^
               
            }
            
            if(oppMagicLast-oppMagic==19){
                    flipendoCasted=4;
                }else{
                    if(flipendoCasted!=0){
                        anyFlipendo=1;
                        flipendoCasted--;
                    }
                }// flipendo detection
            
            for (int i = 0; i < 2; i++)//petla dla dwoch ziomow
            {   
                Console.Error.WriteLine("------------------WIZ "+i+"----------------------");
                int closestSnaffleIndex=0,snaffleOdCo=0,closestdist=20000;
                int snaffToStopId=0, snaffToAccioId=0;
                int s=0;
                for(int j=0;j<snaffles.Length;j++){
                    if(snaffles[j].entityId!=-1 && snaffles[j].entityType=="SNAFFLE"){
                        
                        Console.Error.WriteLine("Snaff:"+ j + " x:"+snaffles[j].x+" y:"+snaffles[j].y+" vx:"+snaffles[j].vx+" vy:"+snaffles[j].vy+" state:"+snaffles[j].state);
                        if(myTeamId==0){
                            if((snaffles[j].x-snaffles[j].vx)>0 && (snaffles[j].x-snaffles[j].vx)<7000){
                                snaffToAccioId=snaffles[j].entityId;
                            }
                            if(snaffles[j].vx<-600 && snaffles[j].vx>-1600 ){
                                snaffToStopId=snaffles[j].entityId;
                            }
                        }else if(myTeamId==1){
                            if((snaffles[j].x-snaffles[j].vx)>9000 && (snaffles[j].x-snaffles[j].vx)<16000){
                                snaffToAccioId=snaffles[j].entityId;
                            }
                            if(snaffles[j].vx>600 && snaffles[j].vx<1600){
                                snaffToStopId=snaffles[j].entityId;
                            }
                        }
                        double dist = Math.Sqrt(Math.Pow(wiz[i].x - snaffles[j].x, 2) + Math.Pow(wiz[i].y - snaffles[j].y, 2));
                        if(closestdist>dist){
                            snaffleOdCo=j;
                            closestdist=(int)dist;
                            closestSnaffleIndex=(int)snaffles[j].entityId;
                            if(snaffleCounter>1){
                                snaffles[j].entityId=-1;
                            }
                            //Console.Error.WriteLine()
                            
                        }
                    }
                 }
                
                 //Console.Error.WriteLine("najblizszy dla"+wiz[i].entityId+" " +snaffleOdCo);
                //target closest snaffle
                Console.Error.WriteLine("Wiz"+ i+ " x:"+wiz[i].x+" y:"+wiz[i].y+" state:"+wiz[i].state);
                int someoneCloserToMySnaffle=2; // 1 if opwizz[1] is closer, 0 if opwizz[0] is closer, 2 when nobody is closer
                if(someoneCloserToMySnaffle==2){
                    if(AmICloser(wiz[i].x,wiz[i].y,oppwiz[0].x,oppwiz[0].y,snaffles[snaffleOdCo].x,snaffles[snaffleOdCo].y)){
                        someoneCloserToMySnaffle=0;
                    }else if(AmICloser(wiz[i].x,wiz[i].y,oppwiz[1].x,oppwiz[1].y,snaffles[snaffleOdCo].x,snaffles[snaffleOdCo].y)){
                        someoneCloserToMySnaffle=1;
                    }else{
                        someoneCloserToMySnaffle=2;
                    }
                }
                // jesli kafel jest blisko mojej bramki / snaffle.x >= 14500-16000 OR snaffle.x <= 0-1500 
                    // jesli moj ziomek jest bardzo daleko od bramki / roznica 8000 / diff =wiz[i].x-snaffle 
                Console.Error.WriteLine("OppMagicLast:"+oppMagicLast+" oppMagic:"+oppMagic);
                Console.Error.WriteLine("enemy flipendo casted "+ flipendoCasted);
                Console.Error.WriteLine("PETRIFICUS TEST:"+"if("+snaffToStopId+"!=0 && "+myMagic+">=10 && "+(snaffles[snaffToStopId].vx*teamModfier)+"<0 && "+petrified[snaffToStopId]+"==0 && "+flipendoCasted+">0");
                
                // if(CanBludgerHit(entities+1,0)>0){
                //     wiz[i].Flipendo(entities+1);
                // }else 
                if(snaffToStopId!=0 && myMagic>=15 && (snaffles[snaffToStopId].vx*teamModfier)<0 && petrified[snaffToStopId]==0 && flipendoCasted>0){
                    Console.Error.WriteLine("Stop that "+snaffToStopId+"!"+ " Dist:"+Distance(wiz[i].x,wiz[i].y,snaffles[snaffToStopId].x,snaffles[snaffToStopId].y));
                    if(Distance(wiz[i].x,wiz[i].y,snaffles[snaffToStopId].x,snaffles[snaffToStopId].y)<5000){
                        wiz[i].Accio(snaffToStopId);
                    }else{
                        wiz[i].Petrificus(snaffToStopId);
                        petrified[snaffToStopId]=1;
                    }
                    
                }else if(!(oppMagic>=20 && myMagic>=45 && anyFlipendo==1) && PowerFlipendo(i,snaffleOdCo)==true && (CanFlipendoGoal(wiz[i].x,wiz[i].y,snaffles[snaffleOdCo].x-snaffles[snaffleOdCo].vx,snaffles[snaffleOdCo].y-snaffles[snaffleOdCo].vy,myTeamId)!=0 )&&(myMagic>=20) &&
                    (((myTeamId==0)&&(wiz[i].x+600<snaffles[snaffleOdCo].x))||((myTeamId==1)&&(wiz[i].x-600>snaffles[snaffleOdCo].x)))
                    ){
                    Console.Error.WriteLine("Flipendo? : "+ CanFlipendoGoal(wiz[i].x,wiz[i].y,(snaffles[snaffleOdCo].x-snaffles[snaffleOdCo].vx),(snaffles[snaffleOdCo].y-snaffles[snaffleOdCo].vy),myTeamId)+" ");        
                    wiz[i].Flipendo(snaffleOdCo);
                    
                
                // }else if((someoneCloserToMySnaffle!=2)&&(myMagic>30)){
                //     wiz[i].Petrificus(oppwiz[someoneCloserToMySnaffle].entityId);
                }else if(snaffToAccioId!=0  && myMagic>=15 && (snaffles[snaffToAccioId].vx*teamModfier)<0 && SnaffleInArea(snaffToAccioId)==true && (EffectiveAccio(i,snaffToAccioId)==true && GimmeThat(i,snaffToAccioId)==true)){
                    wiz[i].Accio(snaffToAccioId);
                    snaffToAccioId=0;
                // }else if((someoneCloserToMySnaffle!=2)&&(myMagic>25)&&(wiz[i].x>snaffles[snaffleOdCo].x)&& myMagic>15 && EffectiveAccio(i,snaffToAccioId)==true){
                //     wiz[i].Accio(snaffleOdCo);
                }else if(wiz[i].state==1){
                     //Console.Error.WriteLine("Wiz"+ i+ " x:"+wiz[i].x+" y:"+wiz[i].y+" state:"+wiz[i].state);
                     if(myTeamId==1){//lewo
                        wiz[i].Throw(0,3750,500);
                        }else{
                            wiz[i].Throw(16000,3750,500);
                        }
                    Console.Error.WriteLine("RZUCAM!");
                    }else {
                        //Console.WriteLine("MOVE "+snaffles[closestSnaffleIndex].x+" "+snaffles[closestSnaffleIndex].y+ " " + "150");
                    
                        wiz[i].Move(snaffles[snaffleOdCo].x,snaffles[snaffleOdCo].y,150);
                        
                        
                    }
                    

            }//zamkniecie petli 2 graczy
            nullifyPetrified();
        }//koniec while
        
        
    }//koniec Main
    
    static void Initialize(){
        oppMagic=102;
        oppMagicLast=102;
        flipendoCasted=0;
        anyFlipendo=0;
        teamModfier=1;
        petrified=new int[7+4+2];
        for(int i=0;i<petrified.Length;i++){
            petrified[i]= 0;
        }
        snaffles=new Snuffles[7+4+2];// 7 snaff + 4 wiz + 2 b
        for(int i=0;i<snaffles.Length;i++){
            snaffles[i]= new Snuffles();
        }
        wiz = new List<Wizards>();
        wiz.Add(new Wizards());
        wiz.Add(new Wizards());
        
        oppwiz = new List<OpponentWizards>();   
        oppwiz.Add(new OpponentWizards());
        oppwiz.Add(new OpponentWizards());
        
    }
    
    static double Distance(int px, int py, int sx, int sy){
        return Math.Sqrt(Math.Pow(px - sx, 2) + Math.Pow(py - sy, 2));
    }
    
    static double DistanceWiz(int pid, int sid){
        return Math.Sqrt(Math.Pow(wiz[pid].x - snaffles[sid].x, 2) + Math.Pow(wiz[pid].y - snaffles[sid].y, 2));
    }
    //Usefull functions by me are below :D
    static bool AmICloser(int px, int py, int opx, int opy, int sx, int sy){
        double distPlayerSnaffle = Math.Sqrt(Math.Pow(px - sx, 2) + Math.Pow(py - sy, 2));
        double distOppPlayerSnaffle = Math.Sqrt(Math.Pow(opx - sx, 2) + Math.Pow(opy - sy, 2));
        if(distPlayerSnaffle<distOppPlayerSnaffle){
            //Console.Error.WriteLine("Jestem blizej");
            return true;
        }else if(distPlayerSnaffle>=distOppPlayerSnaffle){
            //Console.Error.WriteLine("Jestem dalej niz on");
            return false;
        }else{
            // Console.Error.WriteLine("WTF? Gdzie jestem?");
            return true;
        }
    }
    
    static void nullifyPetrified(){
        for(int i=0;i<petrified.Length;i++){
            petrified[i]= 0;
        }
    }
    
    static bool SnaffleInArea(int snaffId){
        if((snaffles[snaffId].x+snaffles[snaffId].vx>0 && snaffles[snaffId].x+snaffles[snaffId].vx<16000)){
            return true;
        }else{
            return false;
        }
    }
    
    static bool EffectiveAccio(int wizId, int snaffId){
        //kiedy Accio jest efektywne?
        // kiedy dystans jest w okolidach 1000...
        Console.Error.Write("Wiz:"+wizId+" Dystans do snaffla "+snaffId+": "+ Distance(wiz[wizId].x,wiz[wizId].y,snaffles[snaffId].x,snaffles[snaffId].y) );
        if(Distance(wiz[wizId].x,wiz[wizId].y,snaffles[snaffId].x,snaffles[snaffId].y)<4000 && Distance(wiz[wizId].x,wiz[wizId].y,snaffles[snaffId].x,snaffles[snaffId].y)>500){
            Console.Error.WriteLine(" Mogę walić Accio!");
            return true;
        }else{
            Console.Error.WriteLine(" Za daleko na Accio!");
            return false;
        }
    }
    
    static bool GimmeThat(int wizId, int snaffId){
        //Console.Error.Write("Wiz:"+wizId+" Dystans: " + Distance(wiz[wizId].x,wiz[wizId].y,snaffles[snaffId].x,snaffles[snaffId].y) );
        // if()
        //jeśli snaffle jest bliżej bramki niż przeciwnik, a ja przeciwnik bliżej niż ja
        if(teamModfier==-1){
            if(Distance(16000,3750,snaffles[snaffId].x,snaffles[snaffId].y) < Distance(oppwiz[0].x,oppwiz[0].y,16000,3750) && //dystans między kaflem a bramką mniejszy niż przeciwnikiem a bramką (piłka bliżej bramki niż przeciwnik)
            (Distance(oppwiz[0].x,oppwiz[0].y,16000,3750)<Distance(wiz[wizId].x,wiz[wizId].y,16000,3750))){ //dystans przeciwnika od bramki mniejszy niż mojego wizarda od bramki (przeciwnik bliżej bramki niż moj wizard)
                Console.Error.WriteLine(" GIMME THAT!");
                return true;                
            }else if(Distance(16000,3750,snaffles[snaffId].x,snaffles[snaffId].y) < Distance(oppwiz[1].x,oppwiz[1].y,16000,3750) &&
            (Distance(oppwiz[1].x,oppwiz[1].y,16000,3750)<Distance(wiz[wizId].x,wiz[wizId].y,16000,3750))){
                Console.Error.WriteLine(" GIMME THAT!");
                return true;
            }else{
                return false;
            }
            
        }else if(teamModfier==1){
            if(Distance(0,3750,snaffles[snaffId].x,snaffles[snaffId].y) < Distance(oppwiz[0].x,oppwiz[0].y,0,3750) &&
            (Distance(oppwiz[0].x,oppwiz[0].y,0,3750)<Distance(wiz[wizId].x,wiz[wizId].y,0,3750))){
                Console.Error.WriteLine(" GIMME THAT!");
                return true;                
            }else if(Distance(0,3750,snaffles[snaffId].x,snaffles[snaffId].y) < Distance(oppwiz[1].x,oppwiz[1].y,0,3750) &&
            (Distance(oppwiz[1].x,oppwiz[1].y,0,3750)<Distance(wiz[wizId].x,wiz[wizId].y,0,3750))){
                Console.Error.WriteLine(" GIMME THAT!");
                return true;
            }
        }
            return false;
    }
    
    static int CanFlipendoGoal(int px, int py, int sx, int sy,int myTeamId){
        int x=0;
        int m=0;
        if(myTeamId==0){
            x=0;
        }else if(myTeamId==1){
            x=16000;
        }
        try {
        m = (py - sy) / (px - sx);
        }catch (DivideByZeroException) {
        m = (py - sy) ;
        }
        int b = sy - (m*sx);
        
        int y= m*x +b;//
        Console.Error.WriteLine("If I FLIPENDOOOOOOO ? x="+x+" y= "+ y); 
        if(y>3000 && y<4500 && y!=3750){
            return y;
        }else{
            return 0;
        }
    }   
        static int CanBludgerHit(int bId, int opWizID){
        int x=0;
        int m=0;
        Console.Error.WriteLine("If I fk him with bludger? ? opWiz["+opWizID+"] Bludger:"+bId+" "+snaffles[bId].entityId); 
        x=oppwiz[opWizID].x;
        try {
        m = (snaffles[bId].y - oppwiz[opWizID].y) / (snaffles[bId].x - oppwiz[opWizID].x);
        }catch (DivideByZeroException) {
        m = (snaffles[bId].y - oppwiz[opWizID].y);
        }
        int b = snaffles[bId].y - (m*snaffles[bId].x);
        
        int y= m*x +b;//
        
        if(y==oppwiz[opWizID].y){
            return y;
        }else{
            return 0;
        }
    }   
    
    
    static bool PowerFlipendo(int wizId,int snaffId){
        double dist = Distance(wiz[wizId].x,wiz[wizId].y,snaffles[snaffId].x,snaffles[snaffId].y);
        if(dist<10){
            return false;
        }
        double dcoef = dist*0.001;
        double power = 6000.0 / (dcoef*dcoef);

        if (power > 1000.0) {
            power = 1000.0;
        }
        
        dcoef = 1.0 / dist;
        power = power / 0.5;
        Console.Error.WriteLine("Wiz["+wizId+"] power @ snaff["+snaffId+"] ="+power);
        return true;
        
    }
    
    static void SetTeam(int myTeamId){
        if(myTeamId==0){
            teamModfier=1;
        }else if(myTeamId==1){
            teamModfier=-1;
        }else{
            Console.Error.WriteLine("Cos sie odjechalo z ustawianiem teamu");
        }
    }
    //Usefull functions by me are above :D
    
    //Collision, Simulation and Prediction Code below!
    
    
    // static void GetOut()
    
    

}//Class Player close

public class Wizards{
    //private int entityId,x,y,vx,vy,state;
    //private string entityType;
    public Wizards(){
    this.entityType="WIZARD";
    }
    public Wizards(int entityId, string entityType, int x, int y, int vx, int vy,int state){
    this.entityId=entityId;
    this.entityType=entityType;
    this.x=x;
    this.y=y;
    this.vx=vx;
    this.vy=vy;
    this.state=state;
    }
    public void UpdateState(int entityId, string entityType, int x, int y, int vx, int vy,int state){
    this.entityId=entityId;
    this.entityType=entityType;
    this.x=x;
    this.y=y;
    this.vx=vx;
    this.vy=vy;
    this.state=state;
    }
    
    public void Move(int x, int y, int thrust){
        Console.WriteLine("MOVE " + (x-this.vx) + " " + (y-this.vy) + " " + thrust+ " LECE!");
    }
    public void Throw(int x, int y, int power){
        Console.WriteLine("THROW " + x + " " + y + " " + power + " RZUCAM!");
    }
    public void Accio(int entityId){
        Console.WriteLine("ACCIO " + entityId);
    }
    public void Flipendo(int entityId){
        Console.WriteLine("FLIPENDO " + entityId + " PIPENDO!");
    }
    public void Petrificus(int entityId){
        Console.WriteLine("PETRIFICUS " + entityId);
    }
    public void FindClosest(){
        
    }
    
    public string entityType {get; set;}
    public int entityId {get; set;}
    public int x {get; set;}
    public int y {get; set;}
    public int vx {get; set;}
    public int vy  {get; set;}
    public int state  {get; set;}
}

public class Snuffles{
    //private int entityId,x,y,vx,vy,state;
    //private string entityType;
    public Snuffles(){
        this.entityType="SNAFFLE";
        this.entityId=-1;
    }
    public void UpdateState(int entityId, string entityType, int x, int y, int vx, int vy,int state){
        this.entityId=entityId;
        this.entityType=entityType;
        this.x=x;
        this.y=y;
        this.vx=vx;
        this.vy=vy;
        this.state=state;
    }
    public Snuffles(int entityId, string entityType, int x, int y, int vx, int vy,int state){
    this.entityId=entityId;
    this.entityType=entityType;
    this.x=x;
    this.y=y;
    this.vx=vx;
    this.vy=vy;
    this.state=state;
    }
    public string entityType {get; set;}
    public int entityId {get; set;}
    public int x {get; set;}
    public int y {get; set;}
    public int vx {get; set;}
    public int vy  {get; set;}
    public int state  {get; set;}
}

public class OpponentWizards{
    //private int entityId,x,y,vx,vy,state;
    //private string entityType;
    public OpponentWizards(){
        this.entityType="OPPONENT_WIZARD";
    }
    public OpponentWizards(int entityId, string entityType, int x, int y, int vx, int vy,int state){
        this.entityId=entityId;
        this.entityType=entityType;
        this.x=x;
        this.y=y;
        this.vx=vx;
        this.vy=vy;
        this.state=state;
    }
    public void UpdateState(int entityId, string entityType, int x, int y, int vx, int vy,int state){
    this.entityId=entityId;
    this.entityType=entityType;
    this.x=x;
    this.y=y;
    this.vx=vx;
    this.vy=vy;
    this.state=state;
    }
    public string entityType {get; set;}
    public int entityId {get; set;}
    public int x {get; set;}
    public int y {get; set;}
    public int vx {get; set;}
    public int vy  {get; set;}
    public int state  {get; set;}
}
