defmodule Darts do
  @type distance :: Float
  defp distance({x, y}) do
    :math.sqrt(x ** 2 + y ** 2)
  end
  @type score :: Integer
  def score({x, y}) do
    case distance({x, y}) do
      a when a > 10 -> 0
      a when 5 < a and a <= 10 -> 1
      a when 1 < a and a <= 5 -> 5
      a when 0 <= a and a <= 1 -> 10
    end
  end
end


Darts.distance({1, 2}) |> IO.puts()
Darts.score({1,2}) |> IO.puts()
