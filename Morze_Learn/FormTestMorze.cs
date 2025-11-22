using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class FormTestMorze : Form
    {
        // список вопросов
        private List<Question> вопросы;
        // индекс текущего вопроса
        private int индексТекущегоВопроса = 0;
        // количество правильных ответов
        private int результат = 0;
        // счетчик заданных вопросов
        private int количествоЗаданныхВопросов = 0;
        // лимит вопросов
        private const int МаксимальноеКоличествоВопросов = 10;

        public FormTestMorze()
        {
            InitializeComponent();

            // инициализация вопросов
            ИнициализироватьВопросы();

            // перемешивание вопросов
            ПеремешатьВопросы();

            // загрузка первого вопроса
            ЗагрузитьВопрос();

            // добавляем обработчик нажатия клавиши Enter в поле ответа
            txtAnswer.KeyDown += TxtAnswer_KeyDown;
        }

        // Метод для инициализации списка вопросов
        private void ИнициализироватьВопросы()
        {
            вопросы = new List<Question>
            {
                // все ваши вопросы
                new Question("Что означает код .- ?", "A"),
                new Question("Что означает код -...", "B"),
                new Question("Что означает код -.-.", "C"),
                new Question("Что означает код -..", "D"),
                new Question("Что означает код .", "E"),
                new Question("Что означает код ..-.", "F"),
                new Question("Что означает код --.", "G"),
                new Question("Что означает код ....", "H"),
                new Question("Что означает код ..", "I"),
                new Question("Что означает код .---", "J"),
                new Question("Что означает код -.-", "K"),
                new Question("Что означает код .-..", "L"),
                new Question("Что означает код --", "M"),
                new Question("Что означает код -.", "N"),
                new Question("Что означает код ---", "O"),
                new Question("Что означает код .--.", "P"),
                new Question("Что означает код --.-", "Q"),
                new Question("Что означает код .-.", "R"),
                new Question("Что означает код ...", "S"),
                new Question("Что означает код -", "T"),
                new Question("Что означает код ..-", "U"),
                new Question("Что означает код ...-", "V"),
                new Question("Что означает код .--", "W"),
                new Question("Что означает код -..-", "X"),
                new Question("Что означает код -.--", "Y"),
                new Question("Что означает код --..", "Z"),
                // цифры
                new Question("Что означает код .---- ?", "1"),
                new Question("Что означает код ..--- ?", "2"),
                new Question("Что означает код ...-- ?", "3"),
                new Question("Что означает код ....- ?", "4"),
                new Question("Что означает код ..... ?", "5"),
                new Question("Что означает код -.... ?", "6"),
                new Question("Что означает код --... ?", "7"),
                new Question("Что означает код ---.. ?", "8"),
                new Question("Что означает код ----. ?", "9"),
                new Question("Что означает код ----- ?", "0"),
                // знаки препинания и спец.символы
                new Question("Что означает код .-.-.- ?", "."),
                new Question("Что означает код --..-- ?", ","),
                new Question("Что означает код ..--.. ?", "?"),
                new Question("Что означает код -.-.-- ?", "!"),
                new Question("Что означает код .-...", ":"),
                new Question("Что означает код -.-.-.", ";"),
                new Question("Что означает код -..-.", "/"),
                new Question("Что означает код -.--.", "'"),
                new Question("Что означает код -.--.", "\""),
                new Question("Что означает код .-...", "="),
                new Question("Что означает код ...-..- ?", "$"),
                new Question("Что означает код .--.-. ?", "@"),
                new Question("Что означает код ...---...", "SOS"),
                new Question("Код для паузы или разделения слов — /", "/")
            };
        }

        // Метод для перемешивания вопросов
        private void ПеремешатьВопросы()
        {
            Random случайный = new Random();
            int длина = вопросы.Count;
            for (int i = 0; i < длина - 1; i++)
            {
                int j = случайный.Next(i, длина);
                var временнаяПеременная = вопросы[i];
                вопросы[i] = вопросы[j];
                вопросы[j] = временнаяПеременная;
            }
        }

        // Метод для загрузки вопроса
        private void ЗагрузитьВопрос()
        {
            if (количествоЗаданныхВопросов >= МаксимальноеКоличествоВопросов || индексТекущегоВопроса >= вопросы.Count)
            {
                ПоказатьРезультаты();
                return;
            }

            if (индексТекущегоВопроса < вопросы.Count)
            {
                lblQuestion.Text = вопросы[индексТекущегоВопроса].Text;
                txtAnswer.Text = "";
                txtAnswer.Focus();
                индексТекущегоВопроса++;
                количествоЗаданныхВопросов++;
            }
        }

        // Обработчик нажатия кнопки "Далее"
        private void BtnNext_Click(object sender, EventArgs e)
        {
            ПроверитьОтветИЗагрузитьСледующий();
        }

        // Метод для проверки ответа и перехода к следующему вопросу
        private void ПроверитьОтветИЗагрузитьСледующий()
        {
            string ответПользователя = txtAnswer.Text.Trim().ToUpper();
            string правильныйОтвет = вопросы[индексТекущегоВопроса - 1].Answer.ToUpper();

            if (ответПользователя == правильныйОтвет)
            {
                результат++;
            }

            ЗагрузитьВопрос();
        }

        // Метод для отображения итогов
        private void ПоказатьРезультаты()
        {
            lblQuestion.Visible = false;
            txtAnswer.Visible = false;
            btnNext.Visible = false;
            progressBar.Visible = false;

            lblResult.Text = $"Тест завершен!\nВаш результат: {результат} из {МаксимальноеКоличествоВопросов}";
            lblResult.Visible = true;
        }

        // Класс вопроса
        private class Question
        {
            public string Text { get; }
            public string Answer { get; }

            public Question(string text, string answer)
            {
                Text = text;
                Answer = answer;
            }
        }

        // Обработчик нажатия клавиши Enter в поле ответа
        private void TxtAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // отключить системное действие Enter
                e.SuppressKeyPress = true; // отключить звуковой сигнал
                ПроверитьОтветИЗагрузитьСледующий(); // проверить ответ и перейти дальше
            }
        }

        // Обработчик для кнопки "Начать заново" (если есть)
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            // сбросить параметры
            индексТекущегоВопроса = 0;
            результат = 0;
            количествоЗаданныхВопросов = 0;

            // восстановить видимость элементов
            lblQuestion.Visible = true;
            txtAnswer.Visible = true;
            btnNext.Visible = true;
            progressBar.Visible = true;
            lblResult.Text = "";
            lblResult.Visible = false;
            progressBar.Value = 0;

            // перемешать вопросы заново
            ПеремешатьВопросы();

            // загрузить первый вопрос
            ЗагрузитьВопрос();
        }
    }
}