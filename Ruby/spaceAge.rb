class SpaceAge
    @@earth = 31557600.0

    def initialize(seconds)
        @seconds = seconds
    end

    def on_earth()
        (@seconds / @@earth).round(2)
    end

    def on_mercury
        (@seconds / (@@earth * 0.240_846_7)).round(2)
    end

    def on_venus
        (@seconds / (@@earth * 0.61519726)).round(2)
    end

    def on_mars
        (@seconds / (@@earth * 1.8808158)).round(2)
    end

    def on_jupiter
        (@seconds / (@@earth * 11.862615)).round(2)
    end

    def on_saturn
        (@seconds / (@@earth * 29.447498)).round(2)
    end

    def on_uranus
        (@seconds / (@@earth * 84.016846)).round(2)
    end

    def on_neptune
        (@seconds / (@@earth * 164.79132)).round(2)
    end
end


age = SpaceAge.new(1_000_000_000)
puts age.on_earth
puts 2 / 5.0