using iTextSharp.text;
using iTextSharp.text.pdf;
using MyLyrics.Logic.Data;
using System.IO;
using System;

namespace MyLyrics.Logic
{
    internal class PdfDocumentCreator
    {
        private readonly Rectangle _pageSizeRectangle;
        private readonly int _numberOfColumns;
        private readonly string _pathToSave;
        private int _fontSize;
        private readonly int _minimumFontSize;
        private readonly string _documentName;

        public PdfDocumentCreator(string path = "")
        {
            _pageSizeRectangle = PageSize.A4.Rotate();
            _numberOfColumns = 2;
            _fontSize = 20;
            _minimumFontSize = 2;
            long miliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            _documentName = $"{miliseconds}.pdf";
            _pathToSave = path + _documentName;
        }

        internal string CreateDocumentForSong(Song song)
        {
            PdfPTable table = TableWithSongLyrics(song);
            while (GetNumberOfPages(table) > 1)
            {
                _fontSize--;
                table = TableWithSongLyrics(song);
            }
            SaveDocument(table);
            return _documentName;
        }

        private int GetNumberOfPages(IElement content)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] documentAsByteArray = GenerateDocumentAndReturnBytes(content, stream);
                PdfReader reader = new PdfReader(documentAsByteArray);
                return reader.NumberOfPages;
            }
        }

        private byte[] GenerateDocumentAndReturnBytes(IElement content, MemoryStream stream)
        {
            GenerateDocument(content, stream);
            return stream.ToArray();
        }

        private void GenerateDocument(IElement content, Stream stream)
        {
            using (Document document = CreateDocument())
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();
                document.Add(content);
                document.Close();
            }
        }

        private Document CreateDocument()
        {
            Document output = new Document(_pageSizeRectangle);
            return output;
        }

        private PdfPTable TableWithSongLyrics(Song song)
        {
            PdfPTable table = CreateTable();

            string headerText = song.Name + " - " + song.Band;
            PdfPCell songHeaderCell = CreateCell(headerText, CreateBoldFont());
            PdfPCell breakLineCell = CreateCell("\n", CreateRegularFont());
            PdfPCell bodyCell = CreateCell(song.Text, CreateRegularFont());

            AddOneCellPerColumn(table, songHeaderCell);
            AddOneCellPerColumn(table, breakLineCell);
            AddOneCellPerColumn(table, bodyCell);
            return table;
        }

        private PdfPTable CreateTable()
        {
            PdfPTable table = new PdfPTable(_numberOfColumns);
            table.WidthPercentage = 100;
            return table;
        }

        private PdfPCell CreateCell(string text, Font font)
        {
            Phrase phrase = new Phrase(text, font);
            PdfPCell output = new PdfPCell(phrase);
            output.Border = 0;
            return output;
        }

        private void AddOneCellPerColumn(PdfPTable table, PdfPCell cell)
        {
            for (int i = 0; i < _numberOfColumns; i++)
            {
                table.AddCell(cell);
            }
        }

        private Font CreateBoldFont()
        {
            Font output = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, _fontSize);
            return output;
        }

        private Font CreateRegularFont()
        {
            Font output = FontFactory.GetFont(FontFactory.HELVETICA, _fontSize);
            return output;
        }


        private void SaveDocument(IElement content)
        {
            using (FileStream stream = new FileStream(_pathToSave, FileMode.Create))
            {
                GenerateDocument(content, stream);
            }
        }
    }
}
