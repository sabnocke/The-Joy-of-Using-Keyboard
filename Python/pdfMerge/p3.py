import PyPDF2
from PyPDF2 import Transformation
import fitz
from typing import Iterable, List, Dict
from pprint import pprint
from Manager import YamlManager, TPathLike


def overlay_pdf(
        path1: TPathLike,
        path2: TPathLike,
        output: TPathLike) -> None:
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

        merged.add_transformation(Transformation().scale(1).translate(0, 0))
        merged.merge_page(page1)
        merged.merge_page(page2)

        output_pdf.add_page(merged)
    with open(output, "wb") as pdf:
        output_pdf.write(pdf)


def change_color(_input: TPathLike,
                 color: Iterable[float] = (1, 0.341, 0.200)) -> set:
    pdf1 = fitz.open(_input)
    font_map = YamlManager("config.yml")["fontPath"]
    fonts: set = set()
    for i in range(pdf1.page_count):
        page = pdf1.load_page(i)
        blocks: List = page.get_text("dict")["blocks"]
        for block in blocks:
            block: Dict
            for line in block["lines"]:
                line: Dict
                for span in line["spans"]:
                    span: Dict
                    if span["font"].lower() not in font_map:
                        # fonts not seen before
                        # will need to use cnnct
                        fonts.add(span["font"].lower())
                    else:
                        font = fitz.Font(fontfile=font_map[span["font"].lower()])
                        tw = fitz.TextWriter(page.rect, color=color)
                        tw.append(pos=span["origin"],
                                  text=span["text"],
                                  font=font,
                                  fontsize=span["size"]
                                  )
                        tw.write_text(page)
    pdf1.ez_save("output.pdf")
    print(fonts)
    return fonts


def main() -> None:
    path1 = r"C:\Users\ReWyn\^\projects\school\ity\ity-projekt2\vzor.pdf"
    path2 = r"C:\Users\ReWyn\^\projects\school\ity-projekt2\proj2.pdf"
    output = r".\output.pdf"
    # overlay_pdf(path1, path2, output)
    change_color(path1)


if __name__ == "__main__":
    main()

# TODO use Google Search API ( or its alternative ) to pass query for fonts found in document
# TODO Download the fonts and update index
# TODO Parse the document, using the downloaded fonts and predefined color
# => Profit
