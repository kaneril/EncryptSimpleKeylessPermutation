﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncryptSimpleKeylessPermutation
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:\n 1 Закодировать текст\n 2 Раскодировать текст\n 3 Завершить работу");
                var str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        EncryptText();
                        break;
                    case "2":
                        DecryptText();
                        break;
                    case "3":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный код действия");
                        break;
                }
            }
        }
        static void EncryptText()
        {
            Console.WriteLine("Введите кол-во столбцов в таблице");
            uint ColumnsNumber = 0;
            try
            {
                ColumnsNumber = UInt32.Parse(Console.ReadLine());
                var i = 1 / ColumnsNumber;
                Console.WriteLine("Введите путь к файлу с кодируемым текстом");
                var path = Console.ReadLine();
                string NormalText = "";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    NormalText = sr.ReadToEnd();
                }
                var NewText = MakeEncryptText(ColumnsNumber, NormalText);
                Console.WriteLine("Введите путь к файлу с закодированным текстом");
                path = Console.ReadLine();
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"{ColumnsNumber}");
                    sw.WriteLine(NewText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }            
        }        
        static string MakeEncryptText(uint ColumnsNumber, string NormalText)
        {
            string NewText = "";
            for (var i=0; i<ColumnsNumber;i++)
            {
                for (var j=0; i+j<NormalText.Length; j+=(int)ColumnsNumber)
                {
                    NewText += NormalText[j + i];
                }
            }
            return NewText;
        }
        static void DecryptText()
        {
            try
            {
                Console.WriteLine("Введите путь к файлу с закодированным текстом");
                var path = Console.ReadLine();
                uint ColumnsNumber = 0;
                string EncryptText = "";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    ColumnsNumber = UInt32.Parse(sr.ReadLine());
                    var i = 1 / ColumnsNumber;
                    EncryptText = sr.ReadToEnd();
                }
                var NewText = MakeDecryptText(ColumnsNumber, EncryptText);
                Console.WriteLine("Введите путь к файлу с раскодированным текстом");
                path = Console.ReadLine();
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(NewText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static string MakeDecryptText(uint ColumnsNumber, string EncryptText)
        {
            string NewText = "";
            for (var i = 0; i < EncryptText.Length / ColumnsNumber; i++)
            {
                for (var j = 0; j < ColumnsNumber; j++)
                {
                    int k = 0;
                    if (j>= EncryptText.Length % ColumnsNumber)
                    {
                        k = j * (EncryptText.Length / (int)ColumnsNumber) + (EncryptText.Length % (int)ColumnsNumber)+i;
                    }
                    else
                    {
                        k = j * (EncryptText.Length / (int)ColumnsNumber)+j+i;
                    }
                    NewText += EncryptText[k];
                }
            }
            for (var i = 0; i < EncryptText.Length % (int)ColumnsNumber; i++)
            {
                NewText += EncryptText[(i + 1) * (EncryptText.Length / (int)ColumnsNumber + 1) - 1];
            }
            return NewText;
        }
    }
}
