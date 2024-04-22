defmodule Year do
  @doc """
  Returns whether 'year' is a leap year.

  A leap year occurs:

  on every year that is evenly divisible by 4
    except every year that is evenly divisible by 100
      unless the year is also evenly divisible by 400
  """
  @spec leap_year?(non_neg_integer) :: boolean
  def leap_year?(year) do
    s = for num <- [4, 100, 400], do: sign(rem(year, num))
    rem(Enum.sum(s), 2) == 0
  end

  @spec sign(integer) :: integer
  defp sign(n) do
    cond do
      n > 0 -> 1
      n == 0 -> 0
      n < 0 -> -1
    end
  end
end
