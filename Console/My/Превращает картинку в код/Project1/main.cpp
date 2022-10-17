#include <vcl.h>
#pragma hdrstop

#include "win_main.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"

TForm1* Form1;
Graphics::TBitmap* bmp = new Graphics::TBitmap;
//---------------------------------------------------------------------------


class intensity
{
public:
    char c;                    // Character
    int il, ir, iu, id, ic;    // Intensity of part: left,right,up,down,center
    intensity() { c = 0; reset(); }
    void reset() { il = 0; ir = 0; iu = 0; id = 0; ic = 0; }

    void compute(DWORD** p, int xs, int ys, int xx, int yy) // p source image, (xs,ys) area size, (xx,yy) area position
    {
        int x0 = xs >> 2, y0 = ys >> 2;
        int x1 = xs - x0, y1 = ys - y0;
        int x, y, i;
        reset();
        for (y = 0; y < ys; y++)
            for (x = 0; x < xs; x++)
            {
                i = (p[yy + y][xx + x] & 255);
                if (x <= x0) il += i;
                if (x >= x1) ir += i;
                if (y <= x0) iu += i;
                if (y >= x1) id += i;

                if ((x >= x0) && (x <= x1) &&
                    (y >= y0) && (y <= y1))

                    ic += i;
            }

        // Normalize
        i = xs * ys;
        il = (il << 8) / i;
        ir = (ir << 8) / i;
        iu = (iu << 8) / i;
        id = (id << 8) / i;
        ic = (ic << 8) / i;
    }
};


//---------------------------------------------------------------------------
AnsiString bmp2txt_big(Graphics::TBitmap* bmp, TFont* font) // Character  sized areas
{
    int i, i0, d, d0;
    int xs, ys, xf, yf, x, xx, y, yy;
    DWORD** p = NULL, ** q = NULL;    // Bitmap direct pixel access
    Graphics::TBitmap* tmp;        // Temporary bitmap for single character
    AnsiString txt = "";            // Output ASCII art text
    AnsiString eol = "\r\n";        // End of line sequence
    intensity map[97];            // Character map
    intensity gfx;

    // Input image size
    xs = bmp->Width;
    ys = bmp->Height;

    // Output font size
    xf = font->Size;   if (xf < 0) xf = -xf;
    yf = font->Height; if (yf < 0) yf = -yf;

    for (;;) // Loop to simplify the dynamic allocation error handling
    {
        // Allocate and initialise buffers
        tmp = new Graphics::TBitmap;
        if (tmp == NULL)
            break;

        // Allow 32 bit pixel access as DWORD/int pointer
        tmp->HandleType = bmDIB;    bmp->HandleType = bmDIB;
        tmp->PixelFormat = pf32bit; bmp->PixelFormat = pf32bit;

        // Copy target font properties to tmp
        tmp->Canvas->Font->Assign(font);
        tmp->SetSize(xf, yf);
        tmp->Canvas->Font->Color = clBlack;
        tmp->Canvas->Pen->Color = clWhite;
        tmp->Canvas->Brush->Color = clWhite;
        xf = tmp->Width;
        yf = tmp->Height;

        // Direct pixel access to bitmaps
        p = new DWORD * [ys];
        if (p == NULL) break;
        for (y = 0; y < ys; y++)
            p[y] = (DWORD*)bmp->ScanLine[y];

        q = new DWORD * [yf];
        if (q == NULL) break;
        for (y = 0; y < yf; y++)
            q[y] = (DWORD*)tmp->ScanLine[y];

        // Create character map
        for (x = 0, d = 32; d < 128; d++, x++)
        {
            map[x].c = char(DWORD(d));
            // Clear tmp
            tmp->Canvas->FillRect(TRect(0, 0, xf, yf));
            // Render tested character to tmp
            tmp->Canvas->TextOutA(0, 0, map[x].c);

            // Compute intensity
            map[x].compute(q, xf, yf, 0, 0);
        }

        map[x].c = 0;

        // Loop through the image by zoomed character size step
        xf -= xf / 3; // Characters are usually overlapping by 1/3
        xs -= xs % xf;
        ys -= ys % yf;
        for (y = 0; y < ys; y += yf, txt += eol)
            for (x = 0; x < xs; x += xf)
            {
                // Compute intensity
                gfx.compute(p, xf, yf, x, y);

                // Find the closest match in map[]
                i0 = 0; d0 = -1;
                for (i = 0; map[i].c; i++)
                {
                    d = abs(map[i].il - gfx.il) +
                        abs(map[i].ir - gfx.ir) +
                        abs(map[i].iu - gfx.iu) +
                        abs(map[i].id - gfx.id) +
                        abs(map[i].ic - gfx.ic);

                    if ((d0 < 0) || (d0 > d)) {
                        d0 = d; i0 = i;
                    }
                }
                // Add fitted character to output
                txt += map[i0].c;
            }
        break;
    }

    // Free buffers
    if (tmp) delete tmp;
    if (p) delete[] p;
    return txt;
}


//---------------------------------------------------------------------------
AnsiString bmp2txt_small(Graphics::TBitmap* bmp)    // pixel sized areas
{
    AnsiString m = " `'.,:;i+o*%&$#@"; // Constant character map
    int x, y, i, c, l;
    BYTE* p;
    AnsiString txt = "", eol = "\r\n";
    l = m.Length();
    bmp->HandleType = bmDIB;
    bmp->PixelFormat = pf32bit;
    for (y = 0; y < bmp->Height; y++)
    {
        p = (BYTE*)bmp->ScanLine[y];
        for (x = 0; x < bmp->Width; x++)
        {
            i = p[(x << 2) + 0];
            i += p[(x << 2) + 1];
            i += p[(x << 2) + 2];
            i = (i * l) / 768;
            txt += m[l - i];
        }
        txt += eol;
    }
    return txt;
}


//---------------------------------------------------------------------------
void update()
{
    int x0, x1, y0, y1, i, l;
    x0 = bmp->Width;
    y0 = bmp->Height;
    if ((x0 < 64) || (y0 < 64)) Form1->mm_txt->Text = bmp2txt_small(bmp);
    else                  Form1->mm_txt->Text = bmp2txt_big(bmp, Form1->mm_txt->Font);
    Form1->mm_txt->Lines->SaveToFile("pic.txt");
    for (x1 = 0, i = 1, l = Form1->mm_txt->Text.Length(); i <= l; i++) if (Form1->mm_txt->Text[i] == 13) { x1 = i - 1; break; }
    for (y1 = 0, i = 1, l = Form1->mm_txt->Text.Length(); i <= l; i++) if (Form1->mm_txt->Text[i] == 13) y1++;
    x1 *= abs(Form1->mm_txt->Font->Size);
    y1 *= abs(Form1->mm_txt->Font->Height);
    if (y0 < y1) y0 = y1; x0 += x1 + 48;
    Form1->ClientWidth = x0;
    Form1->ClientHeight = y0;
    Form1->Caption = AnsiString().sprintf("Picture -> Text (Font %ix%i)", abs(Form1->mm_txt->Font->Size), abs(Form1->mm_txt->Font->Height));
}


//---------------------------------------------------------------------------
void draw()
{
    Form1->ptb_gfx->Canvas->Draw(0, 0, bmp);
}


//---------------------------------------------------------------------------
void load(AnsiString name)
{
    bmp->LoadFromFile(name);
    bmp->HandleType = bmDIB;
    bmp->PixelFormat = pf32bit;
    Form1->ptb_gfx->Width = bmp->Width;
    Form1->ClientHeight = bmp->Height;
    Form1->ClientWidth = (bmp->Width << 1) + 32;
}


//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner) :TForm(Owner)
{
    load("pic.bmp");
    update();
}


//---------------------------------------------------------------------------
void __fastcall TForm1::FormDestroy(TObject* Sender)
{
    delete bmp;
}


//---------------------------------------------------------------------------
void __fastcall TForm1::FormPaint(TObject* Sender)
{
    draw();
}


//---------------------------------------------------------------------------
void __fastcall TForm1::FormMouseWheel(TObject* Sender, TShiftState Shift, int WheelDelta, TPoint& MousePos, bool& Handled)
{
    int s = abs(mm_txt->Font->Size);
    if (WheelDelta < 0) s--;
    if (WheelDelta > 0) s++;
    mm_txt->Font->Size = s;
    update();
}