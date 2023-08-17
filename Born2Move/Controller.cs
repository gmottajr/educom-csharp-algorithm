﻿using BornToMove.Business;
using BornToMove.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Born2Move
{
    internal class Controller
    {
        //private Crud crud;
        private MoveCrud crud;
        private BuMove buMove;
        private View view;

        public Controller(MoveCrud crud = null)
        {
            this.crud = crud;
            view = new View();
            buMove = new BuMove();
        }

        public void Start()
        {
            
            view.Welcome();
            view.ListOrSuggestion();

            string choice = "";

            while (true)
            {
                choice = Console.ReadLine();
                if(choice == "1")
                {
                    Move suggestion = buMove.GetRandomMove();//RandomMove();
                    view.ShowSuggestion(suggestion);
                    break;
                }
                if(choice == "2")
                {
                    List<Move> moves = buMove.GetAllMoves();//crud.readAllMoves();
                    view.ShowMoveList(moves);
                    Move move = PickMove(moves);
                    view.ShowSuggestion(move);
                    break;
                }
            }

            

            Console.WriteLine("Hoe vond je de oefening?");
            Console.WriteLine("Beoordeling: (1-5) ");
            Console.ReadLine();
            Console.WriteLine("Intensiteit: (1-5) ");
            Console.ReadLine();


        }

        /*public Move RandomMove()
        {
            List<Move> allMoves = crud.readAllMoves();

            Random rdm = new Random();
            int randomIndex = rdm.Next(0, allMoves.Count);

            Move move = allMoves[randomIndex];

            return move;
        }*/

        public Move PickMove(List<Move> moves)
        {
            string choice = "";
            Move move = null;
            while(true)
            {
                choice = Console.ReadLine();
                try
                {
                    int index = int.Parse(choice);
                    if(index == 0)
                    {
                        int id = MakeNewMove();
                        move = crud.readMoveById(id);
                        break;
                    }
                    else
                    {
                        move = moves[index - 1];
                        Console.WriteLine("Je hebt gekozen! " + move.name);
                        break;
                    }
                    

                }
                catch(Exception ex) 
                {
                    Console.WriteLine("Geen beweging!");
                }
            }

            return move;
        }

        public int MakeNewMove()
        {
            string name, description;
            int sweatRate;
            Move move = null;

            Console.WriteLine("Maak een nieuwe beweging:");

            Console.WriteLine("Naam:");
            while (true)
            {
                name = Console.ReadLine();
                if (crud.readMoveByName(name) == null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Deze oefening bestaat al");
                }
            }

            Console.WriteLine("Beschrijving:");
            description = Console.ReadLine();

            Console.WriteLine("Hoeveelheid zweet van 1 tot 5:");
            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    sweatRate = int.Parse(input);
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Een cijfer tussen 1 en 5");
                }
            }

            move = new Move { name = name, description = description, sweatRate = sweatRate };

            int id = crud.create(move);

            return id;
        }
    }
}
