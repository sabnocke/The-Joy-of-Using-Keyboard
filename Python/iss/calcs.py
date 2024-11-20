import soundfile as sf
from bokeh.plotting import figure, show
from bokeh.layouts import grid, row
from bokeh.models import BoxZoomTool, PanTool, ResetTool
from bokeh.io import curdoc
import numpy as np


curdoc().theme = "dark_minimal"

s, Fs = sf.read(r'prase.wav')
N = 10731
n = np.arange(10731)
N_half: int = N // 2
k_all = np.arange(N_half + 1)
A = np.zeros([N_half + 1, N])
B = np.zeros([N_half + 1, N])

A[k_all, :] = np.cos(2 * np.pi * k_all[:, np.newaxis] / N * n)  # One cosine
B[k_all, :] = np.sin(2 * np.pi * k_all[:, np.newaxis] / N * n)  # One sin

C = np.matmul(A, s.T)
D = np.matmul(B, s.T)


def lines(line, fig):
    corr = np.corrcoef(s / np.abs(s).max(), line / np.abs(line).max())[0, 1]

    fig.title = f"Re-synthesized sound file via cos() | Correlation: {corr:e}"
    fig.line(x=n, y=s / np.abs(s).max(), line_width=2, legend_label="Original", color="#F79308", alpha=0.5)
    fig.line(x=n, y=line / np.abs(line).max(), line_width=2, legend_label="Re-synthesized", color="#086CF7", alpha=0.5)
    fig.legend.location = "bottom_left"

    return fig


def cos_syn() -> figure:
    fig = figure(
        title="Placeholder",
        x_axis_label='x',
        y_axis_label='y',
        sizing_mode='inherit',
        tools=[BoxZoomTool(), ResetTool(), PanTool(dimensions="width")],
    )
    xs = np.matmul(C.T, A)

    return lines(xs, fig)


def sin_cos_syn() -> figure:
    fig = figure(
        title=f"Placeholder",
        x_axis_label='x',
        y_axis_label='y',
        sizing_mode='inherit',
        tools=[BoxZoomTool(), ResetTool(), PanTool(dimensions="width")],
    )

    xs1 = np.matmul(C.T, A) + np.matmul(D.T, B)

    return lines(xs1, fig)


def C_exp() -> (figure, figure, figure):
    k_all_2 = np.arange(N)
    A_c = np.zeros([N, N], dtype=complex)
    A_c[k_all_2, :] = np.exp(-1j * 2 * np.pi * k_all_2[:, np.newaxis] / N * n)

    X_c = np.matmul(A_c, s.T)

    X_magnitude = np.abs(X_c)
    X_phase = np.angle(X_c)

    fig = figure(
        title=f"|X[k]|",
        sizing_mode='inherit',
        tools=[BoxZoomTool(), ResetTool(), PanTool(dimensions="width")],
    )

    fig.line(x=k_all_2, y=X_magnitude, line_width=2, color="#F79308", alpha=0.5)

    fig2 = figure(
        title=f"arg X[k]",
        sizing_mode='inherit',
        tools=[BoxZoomTool(), ResetTool(), PanTool(dimensions="width")],
    )

    fig2.line(x=k_all_2, y=X_phase, line_width=2, color="#086CF7", alpha=0.5)

    inverse_A_c = np.zeros([N, N], dtype=complex)

    inverse_A_c[k_all_2, :] = np.exp(1j * 2 * np.pi * k_all_2[:, np.newaxis] / N * n)
    Xs_2 = np.real(np.matmul(X_c.T, inverse_A_c))
    fig3 = figure(
        title="Placeholder",
        tools=[BoxZoomTool(), ResetTool(), PanTool(dimensions="width")],
    )
    corr = np.corrcoef(s / np.abs(s).max(), Xs_2 / np.abs(Xs_2).max())[0, 1]
    fig3.title = f"Re-synthesized sound file via comp.exp. | Correlation: {corr:e}"
    fig3.line(x=n, y=s / np.abs(s).max(), line_width=2, legend_label="Original", color="#F79308", alpha=0.5)
    fig3.line(x=n, y=Xs_2 / np.abs(Xs_2).max(), line_width=2, legend_label="Synthesized", color="#086CF7", alpha=0.5)

    return fig, fig2, fig3


if __name__ == "__main__":
    f0 = cos_syn()
    f1 = sin_cos_syn()
    f2, f3, f4 = C_exp()

    grid = grid(
        [
            [row(f0, f1, sizing_mode="stretch_width")],
            [row(f2, f3, sizing_mode="stretch_width")],
            [f4]
        ],
        sizing_mode='stretch_width',
    )
    show(grid)
