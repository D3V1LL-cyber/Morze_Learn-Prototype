using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using YourNamespace;
using static Morze_Learn.MorseRepository;

namespace Morze_Learn
{
    public partial class Form1 : Form
    {
        private MorseRepository _morseRepository;
        private List<MorseSymbol> _currentSymbols;
        private AudioPlayback _audioPlayback;

        // Элементы управления - основные
        private Label _labelApplicationTitle;
        private TextBox _textBoxSearch;
        private ComboBox _comboBoxCategoryFilter;
        private Button _buttonShowAll;
        private DataGridView _dataGridViewSymbolsTable;

        // Элементы управления - панель деталей
        private Panel _panelSymbolDetails;
        private Label _labelDetailSectionTitle;
        private Label _labelSelectedSymbolDisplay;
        private Label _labelMorseCodeDisplay;
        private Label _labelSymbolDescription;
        private PictureBox _pictureBoxSymbolImage;
        private Button _buttonPlaySound;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

        // Элементы управления - статус
        private Label _labelStatusBar;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeComponent()
        {
            this._labelApplicationTitle = new Label();
            this._textBoxSearch = new TextBox();
            this._comboBoxCategoryFilter = new ComboBox();
            this._buttonShowAll = new Button();
            this._dataGridViewSymbolsTable = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this._panelSymbolDetails = new Panel();
            this._labelDetailSectionTitle = new Label();
            this._labelSelectedSymbolDisplay = new Label();
            this._labelMorseCodeDisplay = new Label();
            this._labelSymbolDescription = new Label();
            this._pictureBoxSymbolImage = new PictureBox();
            this._buttonPlaySound = new Button();
            this._labelStatusBar = new Label();
            this._buttonOpenMorzeTest = new Button();

            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewSymbolsTable)).BeginInit();
            this._panelSymbolDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxSymbolImage)).BeginInit();
            this.SuspendLayout();

            // Настройка элементов
            // _labelApplicationTitle
            this._labelApplicationTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this._labelApplicationTitle.ForeColor = Color.DarkBlue;
            this._labelApplicationTitle.Location = new Point(20, 15);
            this._labelApplicationTitle.Size = new Size(500, 35);
            this._labelApplicationTitle.Text = "АЗБУКА МОРЗЕ - ОБУЧАЮЩАЯ ПРОГРАММА";

            // _textBoxSearch
            this._textBoxSearch.ForeColor = Color.Gray;
            this._textBoxSearch.Location = new Point(20, 60);
            this._textBoxSearch.Size = new Size(200, 23);
            this._textBoxSearch.Text = "Поиск символа...";
            this._textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            this._textBoxSearch.Enter += TextBoxSearch_Enter;
            this._textBoxSearch.Leave += TextBoxSearch_Leave;

            // _comboBoxCategoryFilter
            this._comboBoxCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this._comboBoxCategoryFilter.Items.AddRange(new object[] {
                "Все символы",
                "Латинские буквы",
                "Цифры",
                "Знаки препинания",
                "Специальные сигналы",
                "Русские буквы"
            });
            this._comboBoxCategoryFilter.Location = new Point(230, 60);
            this._comboBoxCategoryFilter.Size = new Size(150, 23);
            this._comboBoxCategoryFilter.SelectedIndexChanged += ComboBoxCategoryFilter_SelectedIndexChanged;

            // _buttonShowAll
            this._buttonShowAll.BackColor = Color.SteelBlue;
            this._buttonShowAll.FlatStyle = FlatStyle.Flat;
            this._buttonShowAll.ForeColor = Color.White;
            this._buttonShowAll.Location = new Point(390, 60);
            this._buttonShowAll.Size = new Size(130, 25);
            this._buttonShowAll.Text = "Показать все символы";
            this._buttonShowAll.Click += ButtonShowAll_Click;

            // _dataGridViewSymbolsTable
            this._dataGridViewSymbolsTable.AllowUserToAddRows = false;
            this._dataGridViewSymbolsTable.AllowUserToDeleteRows = false;
            this._dataGridViewSymbolsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this._dataGridViewSymbolsTable.BackgroundColor = Color.White;
            this._dataGridViewSymbolsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridViewSymbolsTable.Columns.AddRange(new DataGridViewColumn[] {
                dataGridViewTextBoxColumn1,
                dataGridViewTextBoxColumn2,
                dataGridViewTextBoxColumn3
            });
            this._dataGridViewSymbolsTable.GridColor = Color.LightGray;
            this._dataGridViewSymbolsTable.Location = new Point(20, 100);
            this._dataGridViewSymbolsTable.Size = new Size(500, 400);
            this._dataGridViewSymbolsTable.ReadOnly = true;
            this._dataGridViewSymbolsTable.RowHeadersVisible = false;
            this._dataGridViewSymbolsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this._dataGridViewSymbolsTable.SelectionChanged += DataGridViewSymbolsTable_SelectionChanged;

            // Колонки таблицы
            dataGridViewTextBoxColumn1.HeaderText = "Символ";
            dataGridViewTextBoxColumn1.Name = "Character";
            dataGridViewTextBoxColumn1.ReadOnly = true;

            dataGridViewTextBoxColumn2.HeaderText = "Код Морзе";
            dataGridViewTextBoxColumn2.Name = "MorseCode";
            dataGridViewTextBoxColumn2.ReadOnly = true;

            dataGridViewTextBoxColumn3.HeaderText = "Описание";
            dataGridViewTextBoxColumn3.Name = "Description";
            dataGridViewTextBoxColumn3.ReadOnly = true;

            // _panelSymbolDetails
            this._panelSymbolDetails.BackColor = Color.AliceBlue;
            this._panelSymbolDetails.BorderStyle = BorderStyle.FixedSingle;
            this._panelSymbolDetails.Location = new Point(540, 100);
            this._panelSymbolDetails.Size = new Size(330, 400);
            this._panelSymbolDetails.Controls.Add(_labelDetailSectionTitle);
            this._panelSymbolDetails.Controls.Add(_labelSelectedSymbolDisplay);
            this._panelSymbolDetails.Controls.Add(_labelMorseCodeDisplay);
            this._panelSymbolDetails.Controls.Add(_labelSymbolDescription);
            this._panelSymbolDetails.Controls.Add(_pictureBoxSymbolImage);
            this._panelSymbolDetails.Controls.Add(_buttonPlaySound);

            // _labelDetailSectionTitle
            _labelDetailSectionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _labelDetailSectionTitle.Location = new Point(10, 15);
            _labelDetailSectionTitle.Size = new Size(300, 20);
            _labelDetailSectionTitle.Text = "Информация о выбранном символе:";

            // _labelSelectedSymbolDisplay
            _labelSelectedSymbolDisplay.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            _labelSelectedSymbolDisplay.ForeColor = Color.DarkBlue;
            _labelSelectedSymbolDisplay.Location = new Point(100, 50);
            _labelSelectedSymbolDisplay.Size = new Size(120, 70);
            _labelSelectedSymbolDisplay.Text = "—";
            _labelSelectedSymbolDisplay.TextAlign = ContentAlignment.MiddleCenter;

            // _labelMorseCodeDisplay
            _labelMorseCodeDisplay.Font = new Font("Segoe UI", 24F);
            _labelMorseCodeDisplay.ForeColor = Color.DarkRed;
            _labelMorseCodeDisplay.Location = new Point(100, 130);
            _labelMorseCodeDisplay.Size = new Size(120, 40);
            _labelMorseCodeDisplay.Text = "—";
            _labelMorseCodeDisplay.TextAlign = ContentAlignment.MiddleCenter;

            // _labelSymbolDescription
            _labelSymbolDescription.Font = new Font("Segoe UI", 10F);
            _labelSymbolDescription.ForeColor = Color.DarkSlateGray;
            _labelSymbolDescription.Location = new Point(20, 190);
            _labelSymbolDescription.Size = new Size(290, 40);
            _labelSymbolDescription.Text = "Выберите символ из таблицы для просмотра подробной информации";
            _labelSymbolDescription.TextAlign = ContentAlignment.MiddleCenter;

            // _pictureBoxSymbolImage
            _pictureBoxSymbolImage.BackColor = Color.White;
            _pictureBoxSymbolImage.BorderStyle = BorderStyle.FixedSingle;
            _pictureBoxSymbolImage.Location = new Point(20, 240);
            _pictureBoxSymbolImage.Size = new Size(120, 120);
            _pictureBoxSymbolImage.SizeMode = PictureBoxSizeMode.Zoom;

            // _buttonPlaySound
            _buttonPlaySound.BackColor = Color.ForestGreen;
            _buttonPlaySound.Enabled = false;
            _buttonPlaySound.FlatStyle = FlatStyle.Flat;
            _buttonPlaySound.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _buttonPlaySound.ForeColor = Color.White;
            _buttonPlaySound.Location = new Point(150, 280);
            _buttonPlaySound.Size = new Size(160, 40);
            _buttonPlaySound.Text = "▶ Воспроизвести звуковой сигнал";
            _buttonPlaySound.Click += ButtonPlaySound_Click;

            // _labelStatusBar
            _labelStatusBar.Font = new Font("Segoe UI", 9F);
            _labelStatusBar.ForeColor = Color.Gray;
            _labelStatusBar.Location = new Point(20, 510);
            _labelStatusBar.Size = new Size(850, 20);
            _labelStatusBar.Text = "Приложение готово к работе. Выберите символ для изучения.";

            // _buttonOpenMorzeTest
            _buttonOpenMorzeTest = new Button();
            _buttonOpenMorzeTest.BackColor = Color.Indigo;
            _buttonOpenMorzeTest.FlatStyle = FlatStyle.Flat;
            _buttonOpenMorzeTest.ForeColor = Color.White;
            _buttonOpenMorzeTest.Location = new Point(691, 12);
            _buttonOpenMorzeTest.Size = new Size(190, 73);
            _buttonOpenMorzeTest.Text = "Тесты по Морзе";
            _buttonOpenMorzeTest.Click += _buttonOpenMorzeTest_Click;

            // Общие настройки формы
            this.BackColor = Color.White;
            this.ClientSize = new Size(893, 561);
            this.Controls.Add(_labelApplicationTitle);
            this.Controls.Add(_textBoxSearch);
            this.Controls.Add(_comboBoxCategoryFilter);
            this.Controls.Add(_buttonShowAll);
            this.Controls.Add(_dataGridViewSymbolsTable);
            this.Controls.Add(_panelSymbolDetails);
            this.Controls.Add(_labelStatusBar);
            this.Controls.Add(_buttonOpenMorzeTest);
            this.Font = new Font("Segoe UI", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Изучение Азбуки Морзе - Обучающее Приложение";

            ((System.ComponentModel.ISupportInitialize)(this._dataGridViewSymbolsTable)).EndInit();
            this._panelSymbolDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxSymbolImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void _buttonOpenMorzeTest_Click(object sender, EventArgs e)
        {
            var formTestMorze = new FormTestMorze();
            formTestMorze.ShowDialog();
        }

        private void InitializeApplication()
        {
            _morseRepository = new MorseRepository();
            _audioPlayback = new AudioPlayback();
            _currentSymbols = _morseRepository.GetAllSymbols();

            // Установка начального значения комбобокса
            _comboBoxCategoryFilter.SelectedIndex = 0;

            RefreshSymbolsTable();
        }

        private void RefreshSymbolsTable()
        {
            _dataGridViewSymbolsTable.Rows.Clear();

            foreach (var symbol in _currentSymbols)
            {
                _dataGridViewSymbolsTable.Rows.Add(
                    symbol.Character,
                    symbol.MorseCode,
                    symbol.Description
                );
            }

            UpdateStatusBar($"Загружено символов для изучения: {_currentSymbols.Count}");
        }

        private void UpdateStatusBar(string statusMessage)
        {
            _labelStatusBar.Text = statusMessage;
        }

        // Обработчики событий
        private void TextBoxSearch_Enter(object sender, EventArgs e)
        {
            if (_textBoxSearch.Text == "Поиск символа...")
            {
                _textBoxSearch.Text = "";
                _textBoxSearch.ForeColor = Color.Black;
            }
        }

        private void TextBoxSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_textBoxSearch.Text))
            {
                _textBoxSearch.Text = "Поиск символа...";
                _textBoxSearch.ForeColor = Color.Gray;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (_textBoxSearch.Text != "Поиск символа..." && !string.IsNullOrEmpty(_textBoxSearch.Text))
            {
                _currentSymbols = _morseRepository.SearchSymbols(_textBoxSearch.Text);
                RefreshSymbolsTable();
                UpdateStatusBar($"Результаты поиска: найдено {_currentSymbols.Count} символов");
            }
            else if (string.IsNullOrEmpty(_textBoxSearch.Text) || _textBoxSearch.Text == "Поиск символа...")
            {
                var selectedCategory = (SymbolCategory)_comboBoxCategoryFilter.SelectedIndex;
                if (selectedCategory == SymbolCategory.All)
                {
                    _currentSymbols = _morseRepository.GetAllSymbols();
                }
                else
                {
                    _currentSymbols = _morseRepository.GetSymbolsByCategory(selectedCategory);
                }
                RefreshSymbolsTable();
                UpdateStatusBar($"Отображены символы категории: {_comboBoxCategoryFilter.SelectedItem}");
            }
        }

        private void ComboBoxCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_comboBoxCategoryFilter.SelectedIndex >= 0)
            {
                var categories = new List<SymbolCategory>
                {
                    SymbolCategory.All,
                    SymbolCategory.LatinLetters,
                    SymbolCategory.Numbers,
                    SymbolCategory.Punctuation,
                    SymbolCategory.SpecialSignals,
                    SymbolCategory.CyrillicLetters,
                };
                var selectedCategory = categories[_comboBoxCategoryFilter.SelectedIndex];

                if (selectedCategory == SymbolCategory.All)
                {
                    _currentSymbols = _morseRepository.GetAllSymbols();
                }
                else
                {
                    _currentSymbols = _morseRepository.GetSymbolsByCategory(selectedCategory);
                }
                RefreshSymbolsTable();
                UpdateStatusBar($"Отображены символы категории: {_comboBoxCategoryFilter.SelectedItem}");
            }
        }

        private void ButtonShowAll_Click(object sender, EventArgs e)
        {
            _currentSymbols = _morseRepository.GetAllSymbols();
            RefreshSymbolsTable();

            _textBoxSearch.Text = "Поиск символа...";
            _textBoxSearch.ForeColor = Color.Gray;
            _comboBoxCategoryFilter.SelectedIndex = 0;

            UpdateStatusBar("Загружены все доступные символы азбуки Морзе");
        }

        private void DataGridViewSymbolsTable_SelectionChanged(object sender, EventArgs e)
        {
            if (_dataGridViewSymbolsTable.SelectedRows.Count > 0)
            {
                var selectedRow = _dataGridViewSymbolsTable.SelectedRows[0];
                var selectedCharacter = selectedRow.Cells["Character"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedCharacter))
                {
                    var selectedSymbol = _morseRepository.GetSymbolByCharacter(selectedCharacter);
                    if (selectedSymbol != null)
                    {
                        UpdateSymbolDetailsDisplay(selectedSymbol);
                    }
                }
            }
        }

        private void UpdateSymbolDetailsDisplay(MorseSymbol symbol)
        {
            _labelSelectedSymbolDisplay.Text = symbol.Character;
            _labelMorseCodeDisplay.Text = symbol.MorseCode;
            _labelSymbolDescription.Text = symbol.Description;

            // Загрузка и отображение изображения символа (если есть)
            try
            {
                if (symbol.HasImage && !string.IsNullOrEmpty(symbol.ImagePath))
                {
                    _pictureBoxSymbolImage.Image = Image.FromFile(symbol.ImagePath);
                }
                else
                {
                    _pictureBoxSymbolImage.Image = null;
                }
            }
            catch (Exception ex)
            {
                _pictureBoxSymbolImage.Image = null;
                UpdateStatusBar($"Ошибка загрузки изображения: {ex.Message}");
            }

            // Включение или отключение кнопки воспроизведения звука
            _buttonPlaySound.Enabled = symbol.HasSound;
            _buttonPlaySound.BackColor = symbol.HasSound ? Color.ForestGreen : Color.Gray;

            UpdateStatusBar($"Выбран символ: {symbol.Character} → Код Морзе: {symbol.MorseCode}");
        }

        private async void ButtonPlaySound_Click(object sender, EventArgs e)
        {
            if (_dataGridViewSymbolsTable.SelectedRows.Count > 0)
            {
                var selectedRow = _dataGridViewSymbolsTable.SelectedRows[0];
                var selectedCharacter = selectedRow.Cells["Character"].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedCharacter))
                {
                    var selectedSymbol = _morseRepository.GetSymbolByCharacter(selectedCharacter);
                    if (selectedSymbol != null && selectedSymbol.HasSound)
                    {
                        try
                        {
                            _buttonPlaySound.Enabled = false;
                            _buttonPlaySound.Text = "⏸ Воспроизведение...";
                            _buttonPlaySound.BackColor = Color.Orange;

                            UpdateStatusBar($"Воспроизведение кода Морзе: {selectedSymbol.MorseCode}");

                            await _audioPlayback.PlayMorseSequenceAsync(selectedSymbol.MorseCode);

                            _buttonPlaySound.Text = "▶ Воспроизвести звуковой сигнал";
                            _buttonPlaySound.BackColor = Color.ForestGreen;
                            UpdateStatusBar($"Воспроизведение завершено: {selectedSymbol.Character}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка воспроизведения звукового сигнала: {ex.Message}",
                                          "Ошибка аудиовоспроизведения",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                            _buttonPlaySound.Text = "❌ Ошибка воспроизведения";
                            _buttonPlaySound.BackColor = Color.Red;
                            UpdateStatusBar("Ошибка при воспроизведении звукового сигнала");
                        }
                        finally
                        {
                            _buttonPlaySound.Enabled = selectedSymbol.HasSound;
                            await Task.Delay(2000);
                            if (_buttonPlaySound.Text == "❌ Ошибка воспроизведения")
                            {
                                _buttonPlaySound.Text = "▶ Воспроизвести звуковой сигнал";
                                _buttonPlaySound.BackColor = selectedSymbol.HasSound ? Color.ForestGreen : Color.Gray;
                            }
                        }
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (_audioPlayback != null)
            {
                _audioPlayback.Dispose();
                _audioPlayback = null;
            }
            UpdateStatusBar("Завершение работы приложения...");
        }
    }
}
