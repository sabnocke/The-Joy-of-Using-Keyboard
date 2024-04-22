defmodule Secret do
  def xor(num) do
    &(Bitwise.bxor(&1, num))
  end

  def band(n) do
    fn num -> Bitwise.band(n, num) end
  end
  def divide(n) do
    fn num -> div(n, num) end
  end
  def combine(first, second) do
    fn n -> second.(first.(n)) end
  end
end

f = Secret.divide(9)
g = Secret.band(7)
h = Secret.combine(f, g)
IO.puts(h.(81))
IO.puts(div(6, 3))
