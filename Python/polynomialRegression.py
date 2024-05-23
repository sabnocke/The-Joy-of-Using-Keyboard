import numpy as np
import matplotlib.pyplot as plt
from sklearn.metrics import r2_score

x_decorations = [655, 598, 535, 479, 437]
y_decorations = [77_130, 61_930, 47_230, 35_930, 28_430]

x_food = [28_430, 46_630, 51_230, 58_730, 65_930, 77_130]
y_food = [23_041.4, 26_604.6, 28_149.9, 29_565.9, 30_842.5, 32_696.8]


def scatter_decorations(x_dec, y_dec):
    model_decorations = np.poly1d(np.polyfit(x_dec, y_dec, 3))
    decorations_line = np.linspace(655, 437, 25)

    plt.scatter(x_dec, y_dec)
    plt.plot(decorations_line, model_decorations(decorations_line))
    print(r2_score(y_dec, model_decorations(x_dec)))
    plt.show()


def scatter_food(x_f, y_f):
    model_food = np.poly1d(np.polyfit(x_f, y_f, 3))
    food_line = np.linspace(28_430, 77_130, 1500)

    food_line_pred = np.linspace(80_000, 90_000, 10_000)

    plt.scatter(x_f, y_f, label="Data")
    # plt.scatter(new_x, y_pred, "ro")
    plt.plot(food_line, model_food(food_line))
    plt.plot(food_line_pred, model_food(food_line_pred), "r", label="Predictions")
    plt.legend()
    print(r2_score(y_f, model_food(x_f)))
    plt.show()


if __name__ == "__main__":
    scatter_food(x_food, y_food)
