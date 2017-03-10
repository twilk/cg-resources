using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{   
    static string[,] alph;
    static string[,] output;
    static void Main(string[] args)
    {
        
        int L = int.Parse(Console.ReadLine());
        int H = int.Parse(Console.ReadLine());
        int LL=L*28;
        alph=new string[LL,H];
        
        string T = Console.ReadLine();
        byte[] TBytes = Encoding.ASCII.GetBytes(T);
        int TL=T.Length*L;
        int tCounter=0;
        output=new string[TL*4,H];
        for (int i = 0; i < H; i++)
        {
            string ROW = Console.ReadLine();
            int j=0;
            //Console.Error.WriteLine("ROW"+ROW.Length+ "LL="+LL);
            
            foreach(char c in ROW){
                alph[j,i]=c.ToString();
                j++;
                
            }
        }
        for (int i = 0; i < H; i++) {
            for (int j = 0; j < LL ; j++) {
                //Console.Error.Write(alph[j,i]);
            }
            //Console.Error.WriteLine("");
        }
        
        
        
        foreach(int iti in TBytes){
            int ti=iti;
            if(ti>=97 && ti<=122){
                ti=ti-97;
                Console.Error.WriteLine(ti);
            for (int i = 0; i < H; i++) {
                for (int j = 0; j < L ; j++) {
                    output[j+(tCounter*L),i]=alph[ti*L+j,i];
                }
            }
            }else if(ti>=65 && ti<=90){
                ti=ti-65;
                //jesli ti=4
                //output powinien byc na [4*3,i]
                
                Console.Error.WriteLine(ti);
             for (int i = 0; i < H; i++) {
                for (int j = 0; j < L ; j++) {
                    
                    output[j+(tCounter*L),i]=alph[ti*L+j,i];
                }
            }   
            }else{
            for (int i = 0; i < H; i++) {
                for (int j = 0; j < L ; j++) {
                    output[j+(tCounter*L),i]=alph[26*L+j,i];
                }
            }
            }
            Console.Error.WriteLine((char)iti+": tCounter:"+tCounter+" ti=" +ti+" costam"+ti+L*tCounter );
            tCounter++;
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        for (int i = 0; i < H; i++) {
            for (int j = 0; j < TL ; j++) {
                Console.Write(output[j,i]);
            }
            Console.WriteLine("");
        }
        //Console.WriteLine("answer");
    }
    // static void ShowLetter()
}
