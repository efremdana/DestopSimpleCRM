using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using Clients;
using Documents;

namespace Crm2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        LID lid;

        //Коллекция для хранения используемых номеров
        List<int> numberLids = new List<int>();

        private void AddLead_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Text != string.Empty)
            {
                try
                {
                    int num = int.Parse(NumberText.Text);
                    if (!numberLids.Contains(num))
                    {
                        //Если номер не занят, то создается объект типа LID и добовляются анонимные методы для событий
                        //также запускается таймер
                        lid = new LID(NameText.Text, num);
                        InfoText.Text = lid.ToString();
                        lid.TimeWait += (o, args) =>
                        {
                            LabelTimeWait.Content =
                            string.Format("Время ожидания подписанного контракта: {0}", args.TimeWait);
                        };
                        lid.ContractSigned += (o, args) =>
                        {
                            string message = $"Договор с {lid.Name} подписан";
                            string caption = "Сообщение";
                            MessageCreat(message, caption);
                            lid.Sell.Contract.Status = statusContract.Success;
                        };
                        lid.StartWaitingContract();
                    }
                    else
                    {
                        //Если номер уже занят
                        string message = "Введенный номер уже используется другим клиентом";
                        string caption = "Error";
                        MessageCreat(message, caption);
                    }
                }
                catch (FormatException)
                {
                    //Если было введено недопустимое значение дял номера
                    string message = "Недопустимые символы в строке Number";
                    string caption = "Ошибка ввода данных";
                    MessageCreat(message, caption);
                }
            }
            else
            {
                string message = "Строка Name пустая";
                string caption = "Ошибка ввода данных";
                MessageCreat(message, caption);
            }
        }

        private void ConvertLead_Click(object sender, RoutedEventArgs e)
        {
            if (lid != null)
            {
                //Проверка, что договор подписан и можно начинать работу
                if (lid.Sell.Contract.Status == statusContract.Success)
                {
                    if (ProjectNameText.Text != string.Empty)
                    {
                        //Создаётся объект типа Client для дальнейшей работы
                        //также клиент добавляется в базу
                        Client client;
                        client = new Client(lid.Name, lid.Number, ProjectNameText.Text, lid.Sell);
                        lid = client;
                        InfoText.Text += " - стал клиентом";
                        string path = "BaseClient.txt";
                        client.AddToDataBase(path);

                    }
                    else
                    {
                        string message = "Строка Project пустая";
                        string caption = "Ошибка ввода данных";
                        MessageCreat(message, caption);
                    }
                }
                else
                {
                    string message = "Следует дождаться подписанного договора";
                    string caption = "Договор ещё не подписан";
                    MessageCreat(message, caption);
                }
            }
            else
            {
                string message = "Сначала следует создать Лида";
                string caption = "Лид не был создан";
                MessageCreat(message, caption);
            }
        }

        private void StatusProject_Click(object sender, RoutedEventArgs e)
        {
            //получение статуса проекта клиента
            try
            {
                InfoText.Text += $"\nСтатус проекта - {Client.GetStatusProject((Client)lid, lid.Sell.Project)}";
            }
            catch (NullReferenceException)
            {
                string message = "Не был создан клиент";
                string caption = "Error";
                MessageCreat(message, caption);
            }
            catch (InvalidCastException)
            {
                string message = "Не был создан клиент";
                string caption = "Error";
                MessageCreat(message, caption);
            }
            catch (KeyNotFoundException)
            {
                InfoText.Text += "\nПроект не найден";
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lid.Sell.Project.Status == statusProject.Finish)
                {
                    //Если проект завершен, то заканчивается работа с клиентом 
                    numberLids.Add(lid.Number);
                    InfoText.Text += "\nКлиент добавлен в базу";
                    lid.Dispose();
                }
                else
                {
                    string message = "Статус работы можно посмотреть, нажав на соответсвующую кнопку";
                    string caption = "Проект ещё не завершен";
                    MessageCreat(message, caption);
                }
            }
            catch (NullReferenceException)
            {
                string message = "Не был создан клиент";
                string caption = "Error";
                MessageCreat(message, caption);
            }
        }
        //Метод для создания информационного окна
        private static void MessageCreat(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            System.Windows.Forms.MessageBox.Show(message, caption, buttons);
        }
    }
}
