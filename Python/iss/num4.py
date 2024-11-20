import math

import numpy as np
from icecream import ic
from bokeh.plotting import figure, show, column
from bokeh.io import curdoc
from mpmath import mp, cos, pi
from math import exp

curdoc().theme = "dark_minimal"
N = 256

mp.dps = 128
n = mp.arange(0, N // 2)
a = list(map(lambda i: cos(2 * pi * (8 * pi) / 256 * i), n))
b = list(map(lambda i: cos(2 * pi * (8 * pi) / 256 * i), mp.arange(N // 2, N)))
Va = np.array(a, dtype=np.float64)
Vb = np.array(b, dtype=np.float64)

fig = figure(
    title="Cyclicity of cos",
    sizing_mode="inherit",
)
Vn = np.arange(0, N)
fig.line(x=Vn[0:N // 2], y=Va, legend_label="[0,128)", line_width=2, color="#F79308", alpha=0.5)
fig.line(x=Vn[0:N // 2], y=Vb, legend_label="[128,256)", line_width=2, color="#086CF7", alpha=0.5)
fig.line(x=Vn[0:N // 2], y=0, legend_label="0", line_width=2, color="#FFFFF0", alpha=0.5)

fig2 = figure(
    title="x[n] vs a[n]",
    sizing_mode="inherit",
)
Vy = np.cos(2 * np.pi * (8 * np.pi) / 256 * Vn)
Vx = np.ones(N) * 6
Vx2 = np.zeros(N)
Vx2[0] = 6

fig2.line(x=Vn, y=Vy,
          legend_label=f"a[n] | std: {np.std(Vy):.4f}", line_width=2, color="#F79308", alpha=0.5)
fig2.line(x=Vn, y=6, line_width=2, color="#086CF7", alpha=0.5, legend_label=f"x[n] | std: {np.std(Vx):.4f}")

Vyx = np.dot(Vy, Vx.T)
Vyx2 = np.dot(Vy, Vx2.T)

xs = np.dot(Vy, Vyx)
xs2 = np.dot(Vy, Vyx2)
ic(
    np.corrcoef(Vy, xs)[0, 1],
    np.corrcoef(Vy, xs)[0, 1]
)

fig2.line(x=Vn, y=xs, line_width=2, color="#D2122E", alpha=0.5,
          legend_label=f"x[n] synthesized | std: {np.std(xs):.4f}")

fig3 = figure(
    title="x[n] vs a[n]",
    sizing_mode="inherit",
)

fig3.line(x=Vn, y=Vy,
          legend_label=f"a[n] | std: {np.std(Vy):.4f}", line_width=2, color="#F79308", alpha=0.5)
fig3.line(x=Vn, y=Vx2, line_width=2, color="#7FFF00", alpha=0.5, legend_label=f"x[n] | std: {np.std(Vx2):.4f}")
fig3.line(x=Vn, y=xs2, line_width=2, color="#FF5733", alpha=0.5,
          legend_label=f"x[n] synthesized | std: {np.std(xs2):.4f}")

ic(cor)
if __name__ == "__main__":
    show(column(
        fig, fig2, fig3, sizing_mode="stretch_width",
    ))

