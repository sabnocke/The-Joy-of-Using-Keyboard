
a::Array{Int64} = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1]
b::Array{String} = ["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"]

function roman(number)
    if number < 0 || number > 3999
        throw(ErrorException(""))
    end
    result = ""
    for i in eachindex(a)
        div = number ÷ a[i]
        result *= b[i] ^ div
        number %= a[i]
    end
    return result
end

println(roman(402))