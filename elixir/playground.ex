defmodule Wut do
  def t(n) do
    f = x -> Float.round(x, 0)
    g = x -> trunc(x)
    g(f(n))
  end
end


IO.puts(Wut.t(15.5))
