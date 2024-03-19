import PyPDF2
from PyPDF2 import Transformation
import fitz
from typing import Iterable
from pprint import pprint


def overlay_pdf(path1: str, path2: str, output: str) -> None:
    pdf1 = PyPDF2.PdfReader(open(path1, "rb"))
    pdf2 = PyPDF2.PdfReader(open(path2, "rb"))

    output_pdf = PyPDF2.PdfWriter()

    for page in range(min(len(pdf1.pages), len(pdf2.pages))):
        page1 = pdf1.pages[page]
        page2 = pdf2.pages[page]

        content_height = max(page1.mediabox.height, page2.mediabox.height)
        content_width = max(page1.mediabox.width, page2.mediabox.width)

        merged = PyPDF2.PageObject.create_blank_page(
            width=content_width, height=content_height
        )

        # merged.mergeScaledTranslatedPage(page1, 1, 0, 0)
        # merged.mergeScaledTranslatedPage(page2, 1, 0, 0)

        merged.add_transformation(Transformation().scale(1).translate(0, 0))
        merged.merge_page(page1)
        merged.merge_page(page2)

        output_pdf.add_page(merged)
    with open(output, "wb") as pdf:
        output_pdf.write(pdf)


def change_color(_input: str, _output: str, color: Iterable[float] = (0, 0, 0)) -> None:
    pdf1 = fitz.open(_input)
    pdf2 = fitz.open()

    page = pdf1.load_page(1)
    blocks = page.get_text("dict")["blocks"]
    print(len(blocks))
    for b in blocks:
        print(len(b["lines"]))
        print(len(b["lines"][0]["spans"]))
        print(len(b["lines"][0]["spans"][0]))
        # a, c = b["lines"][0]["spans"], b["lines"][1]["spans"]
        # print(a[0]["text"])
        # print(c[0]["text"])
    b: dict = blocks[0]

    font: str = b["lines"][0]["spans"][0]["font"]
    text: str = b["lines"][0]["spans"][0]["text"]
    span = b["lines"][0]["spans"][0]
    count = len(b["lines"])

    # pprint(b["lines"])
    # page.apply_redactions()
    # pprint(span)
    return
    ffont = fitz.Font(fontfile=r".\lmromancaps10-regular.otf")
    tw = fitz.TextWriter(page.rect, color=(1, 0.341, 0.200))
    tw.append(span["origin"], text=text, font=ffont, fontsize=span["size"])
    tw.write_text(page)
    pdf1.ez_save("1.pdf")

    return

    for number in range(len(pdf1)):
        page = pdf1.load_page(number)
        text = page.get_text()

        for block in text:
            for line in block:
                line.set_color(*color)


def main() -> None:
    path1 = r"C:\Users\ReWyn\^\projects\school\ity-projekt2\vzor.pdf"
    path2 = r"C:\Users\ReWyn\^\projects\school\ity-projekt2\proj2.pdf"
    output = r".\output.pdf"
    # overlay_pdf(path1, path2, output)
    change_color(path1, output, color=(255, 0, 0))

if __name__ == "__main__":
    main()
