import tkinter
import tkinter as tkt
import math
import evaluate as ev


# because of how certain calculations are handled "import math" is necessary, although it doesn't do anything here

class Calculator(tkt.Tk):
    def __init__(self):
        super().__init__()
        # config
        self.title("Calculator")
        self.geometry("400x500")
        self.resizable(width=False, height=False)
        self.own_memory = 0  # Ans memory
        self.func: bool = False  # if any advanced function was used
        self.equal_clicked: bool = False  # if equal (=) was clicked
        # input part of screen
        self.question = tkt.Text(self, wrap=tkt.WORD, height=1.5, width=48)
        self.question.place(relx=0.5, rely=0.04, anchor="n")
        self.question.tag_config("question", font=('Arial', 14), justify=tkt.LEFT)
        # output part of screen
        self.answer = tkt.Text(self, wrap=tkt.WORD, height=1.5, width=48)
        self.answer.place(relx=0.5, rely=0.09, anchor="n")
        self.answer.config(height=1.5, width=48, relief=tkt.RIDGE)
        self.answer.tag_config("answer", font=('Arial', 15, "bold"), justify=tkt.RIGHT)
        # setups button grid layout ( where buttons are placed )
        self.button_grid = tkinter.Frame()
        self.button_grid.place(relx=0.5, rely=0.65, anchor="center")
        self.general_width = 20
        self.general_height = 10
        self.general_size = 15
        # call to create all the buttons
        self.create_button("<<", 0, 5, 14, 1, self.general_size)
        self.create_button("sin", 0, 1, 14, 1, self.general_size)

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

    def update_result(self):
        """updates result ( self.answer ) each time a button is pressed"""
        try:
            if not self.func:
                self.answer.delete(1.0, tkt.END)
                _data = self.question.get(1.0, tkt.END)
                _data = round(eval(_data), 4)
                self.answer.insert(tkt.INSERT, _data)
            else:
                self.answer.delete(1.0, tkt.END)
                obj = ev.return_func(self.question.get(1.0, tkt.END))
                self.answer.insert(tkt.INSERT, eval(obj))
        except SyntaxError:
            self.answer.insert(tkt.INSERT, "")

    def button_click(self, text):
        """actual setting of what each button does"""
        if text == "C":
            self.answer.delete(1.0, tkt.END)
            self.question.delete(1.0, tkt.END)
        elif self.equal_clicked:
            self.question.delete(1.0, tkt.END)
            self.question.insert(tkt.INSERT, text)
            self.equal_clicked = False
        elif text == "<<":
            output = self.question.get(1.0, tkt.END)
            output = ''.join(ev.pop_newline(_list=list(output)))
            self.question.delete(1.0, tkt.END)
            self.question.insert(tkt.END, output)
        elif text == "=":
            data = self.question.get(1.0, tkt.END)
            self.own_memory = eval(data)
            self.equal_clicked = True
        elif text == "sin":
            self.func = True
            self.question.insert(tkt.INSERT, f"{text}(")
            self.question.tag_add("question", 1.0, tkt.END)
        elif text == "Ans":
            self.question.insert(tkt.INSERT, f"")
            self.question.insert(tkt.INSERT, f"{self.own_memory}")
            self.update_result()
        else:
            self.question.insert(tkt.INSERT, text)
        self.update_result()
        self.question.tag_add("question", 1.0, tkt.END)
        self.answer.tag_add("answer", 1.0, tkt.END)


calculator: Calculator = Calculator()
_pi = math.pi
# calculator.bind("<Configure>", calculator.config)
calculator.mainloop()
