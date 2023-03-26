import customtkinter

customtkinter.set_appearance_mode("system")
customtkinter.set_default_color_theme("dark-blue")

root = customtkinter.CTk()

root.geometry("500x500")

def login():
    print("Test")
    
frame = customtkinter.CTkFrame(master=root)
frame.pack(pady=20, padx=60, fill="both", expand=True)

label = customtkinter.CTkLabel(frame, text="System Login",)
label.pack(pady=12, padx=10)

entry1 = customtkinter.CTkEntry(frame, )
entry1.pack(pady=12, padx=10)
entry2 = customtkinter.CTkEntry(frame, show="*")
entry2.pack(pady=12, padx=10)

button = customtkinter.CTkButton(frame, text="Login", command=login)
button.pack(pady=12, padx=10)

checkbox = customtkinter.CTkCheckBox(frame, text="Remember me")
checkbox.pack(pady=12, padx=10)

root.mainloop()