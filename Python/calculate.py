import tkinter
import tkinter as tkt
from sympy import *


class Calculator(tkt.Tk):
    def __init__(self):
        super().__init__()

        self.title("Calculator")
        self.geometry("400x500")
        self.resizable(width=False, height=False)
        self.own_memory = 0
        self.clicked = False

        self.question = tkt.Text(self, wrap=tkt.WORD, height=1.5, width=48)
        self.question.place(relx=0.5, rely=0.04, anchor="n")
        self.question.tag_config("question", font=('Arial', 14), justify=tkt.LEFT)

        self.answer = tkt.Text(self, wrap=tkt.WORD, height=1.5, width=48)
        self.answer.place(relx=0.5, rely=0.09, anchor="n")
        self.answer.config(height=1.5, width=48, relief=tkt.RIDGE)
        self.answer.tag_config("answer", font=('Arial', 15, "bold"), justify=tkt.RIGHT)

        self.button_grid = tkinter.Frame()
        self.button_grid.place(relx=0.5, rely=0.65, anchor="center")
        self.general_width = 5
        self.general_height = 5
        self.general_size = 12

        self.create_button("<<", 0, 5, 1, 0.25, self.general_size)

        self.create_button("1", 3, 1, self.general_width, self.general_height, self.general_size)
        self.create_button("2", 3, 2, self.general_width, self.general_height, self.general_size)
        self.create_button("3", 3, 3, self.general_width, self.general_height, self.general_size)
        self.create_button("+", 3, 4, self.general_width, self.general_height, self.general_size)
        self.create_button("-", 3, 5, self.general_width, self.general_height, self.general_size)

        self.create_button("4", 2, 1, self.general_width, self.general_height, self.general_size)
        self.create_button('5', 2, 2, self.general_width, self.general_height, self.general_size)
        self.create_button('6', 2, 3, self.general_width, self.general_height, self.general_size)
        self.create_button("*", 2, 4, self.general_width, self.general_height, self.general_size)
        self.create_button("/", 2, 5, self.general_width, self.general_height, self.general_size)

        self.create_button('7', 1, 1, self.general_width, self.general_height, self.general_size)
        self.create_button('8', 1, 2, self.general_width, self.general_height, self.general_size)
        self.create_button('9', 1, 3, self.general_width, self.general_height, self.general_size)
        self.create_button("(", 1, 4, self.general_width, self.general_height, self.general_size)
        self.create_button(")", 1, 5, self.general_width, self.general_height, self.general_size)

        self.create_button("C", 4, 4, self.general_width, self.general_height, self.general_size)
        self.create_button("0", 4, 1, self.general_width, self.general_height, self.general_size)
        self.create_button(".", 4, 2, self.general_width, self.general_height, self.general_size)
        self.create_button("Ans", 4, 3, self.general_width, self.general_height, self.general_size)
        self.create_button("=", 4, 5, self.general_width, self.general_height, self.general_size)

    def create_button(self, text, row, column, xpad, ypad, font_size):
        button = tkt.Button(self.button_grid, text=text, padx=xpad, pady=ypad, command=lambda: self.button_click(text))
        button.config(font=('Arial', font_size))
        button.grid(row=row, column=column)

    def update_result(self, data):
        try:
            self.answer.delete(1.0, tkt.END)
            self.answer.insert(tkt.END, eval(data))
        except SyntaxError:
            self.answer.insert(tkt.END, "")

    def button_click(self, text):
        if text == "C":
            self.answer.delete(1.0, tkt.END)
            self.question.delete(1.0, tkt.END)
        elif text == "<<":
            output = self.question.get(1.0, tkt.END)
            self.question.delete(1.0, tkt.END)
            self.answer.insert(tkt.END, output[:-1])
            self.question.tag_add("question", 1.0, tkt.END)
        else:
            self.question.insert(tkt.INSERT, text)
            self.question.tag_add("question", 1.0, tkt.END)
            self.update_result(self.question.get(1.0, tkt.END))

    def config(self, events):
        """configs button  + entry"""
        window_width = self.winfo_width()
        button_width = int(window_width * 0.015)
        button_height = int(button_width * 0.5)
        for widget in self.button_grid.winfo_children():
            if isinstance(widget, tkt.Button):
                widget.config(width=button_width, height=button_height)


calculator: Calculator = Calculator()
calculator.bind("<Configure>", calculator.config)
calculator.mainloop()
