
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;


namespace hangmanGame
{
    [Activity(Label = "hangmanwords", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btna;
        Button btnb;
        Button btnc;
        Button btnd;
        string tempword, guessArray; int i;
        string aLetter; string bLetter; string cLetter; string dLetter;
        ListView lstToDoList;
        ListView GuessWord;
        List<ToDo> myList;
        static string dbName = "hangmanwords.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DatabaseManager objDb;
        string selectedWord;
        char asterickChar;
        Boolean clickable;
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
       
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
         //   asterickChar = FindViewById<TextView>(Resource.Id.GUESSWORD);
            //SET TEXTVIEW
            TextView GuessWord = (TextView)FindViewById(Resource.Id.GUESSWORD);

            lstToDoList = FindViewById<ListView>(Resource.Id.listView1);
            lstToDoList.ItemClick += lstToDoList_ItemClick;
            CopyDatabase();

            objDb = new DatabaseManager();
            myList = objDb.ViewAll();
            selectedWord = myList[0].WORD; // 1:: EXTRACT RANDOM WORD - from the list of words CALLED from dbmanager : db command line
            Console.WriteLine("I'm working  WORD...." + selectedWord);
            wordFillStars();
            letterSelect();
            lstToDoList.Adapter = new DataAdapter(this, myList);
           //
            //StringSplit2();
            InitializeControls();
        }


        //  2:: FILL blankWord array with **** same length as random WORD & POPULATE TextView:GUESSWORD
        //DONE
        public void wordFillStars()
        {
            char[] blankWord = selectedWord.ToCharArray();
            Console.WriteLine("WORD IS in BLANKWORD " + selectedWord);
           // foreach (char ch in blankWord)
           // {
                string newString = new string('*', blankWord.Length);
                Console.WriteLine("char in newstring array" + newString);
                //var newMessage = new String('*', message.Length);

                FindViewById<TextView>(Resource.Id.GUESSWORD).Text = newString;
           // }

        }
        // 3:: SELECT LETTER FROM ALPHA compare with all letters in selectWord Array
        private void letterSelect()
        {

            
           // Console.WriteLine("WORD IS in checkword " + checkword);
            for (i = 0; i <= selectedWord.Length; i++)
            {
                string d ="abcdefghijklmnopqrstuvwxyz";
                char[] checkword = selectedWord.ToCharArray();
                Console.WriteLine("CHAR CHECKWORD  " + checkword);
                foreach (char c in checkword)
                    {
                        if (checkword.Equals(d))
                        {
                            Console.WriteLine("LETTER IN CHAR CHECKWORD  " + checkword[i]);
                        }


                //string newString = new string('*', blankWord.Length);
                //Console.WriteLine("char in newstring array" + newString);
                //var newMessage = new String('*', message.Length);

                //FindViewById<TextView>(Resource.Id.GUESSWORD).Text = newString;
                // 
                }
                
                
                
                //if (selectedWord[i] = checkword )
                //{
                //    Console.WriteLine("LETTER IN CHAR CHECKWORD  " + checkword);
                //}
            }

        }



        // 3. CLICK LETTER & CHECK AGAINST selected ARRAY:: 
        //    If letter equals a letter in array then 
        //       a> replace wordFillStars with Letter
        //       b> nohang counter add 1
        //    If nohang == 0 then
        //       a> hangcount counter add 1
        //       b> replace hangman image1 with hangman image 2 (count is 6 chances for 6 images)
       

       

        private void onOneClick(object sender, EventArgs e)
        {
            clickable = false;
            if (clickable = false)
            {
               // btnd hide;
            }
            dLetter = "d";
            Toast.MakeText(this, "LETTER " + dLetter, ToastLength.Long).Show();
            letterSelect();
            
        }

        //void onBtndClick(object sender, EventArgs e)  // ALPHABET::: THIS JUST DISPLAYS IT ON THE SCREEN AT THE BOTTOM FOR A SECOND eg. LETTER d 
        //{
        //    dLetter = "d";
        //    Toast.MakeText(this, "LETTER " + dLetter, ToastLength.Long).Show();
        //    wordFillStars();
        //    onBtndClick.Enabled = false;
        //}
        

        
        public void InitializeControls()
        {
            btna = FindViewById<Button>(Resource.Id.btna);
            btnb = FindViewById<Button>(Resource.Id.btnb);
            btnc = FindViewById<Button>(Resource.Id.btnc);
            btnd = FindViewById<Button>(Resource.Id.btnd);
            btna.Click += onBtnaClick;
            btnb.Click += onBtnbClick;
            btnc.Click += onBtncClick;
            btnd.Click += onOneClick;
        }

        //wrong for calling letters as they need to be disabled onclick - needs work of course: out of time
        void onBtncClick(object sender, EventArgs e)
        {
            cLetter = "c";
            Toast.MakeText(this, "LETTER " + cLetter, ToastLength.Long).Show();
        }

        void onBtnbClick(object sender, EventArgs e)
        {
            bLetter = "b";
            Toast.MakeText(this, "LETTER " + bLetter, ToastLength.Long).Show();
        }

        void onBtnaClick(object sender, EventArgs e)
        {
            aLetter = "a";
            Toast.MakeText(this, "LETTER " + aLetter, ToastLength.Long).Show();
        }

        //not used remove Jaimie!
        void lstToDoList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Console.WriteLine("I'm working");
            Toast.MakeText(this, myList[e.Position].WORD, ToastLength.Long);

        }
        public void CopyDatabase()
        {
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }

                    }

                }

            }
        }

    }
}

