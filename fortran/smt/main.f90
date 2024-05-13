module reverse_string
    implicit none
contains
    function reverse(input) result(reversed)
        character(*), intent(in) :: input
        character(len=len(input)) :: reversed
        integer :: i

        reversed = ""
        do i = len(input), 1, -1
            reversed = trim(reversed)//input(i : i)
        end do
        reversed = reversed
    end function
end module

program smt
    use reverse_string
    write(*, *) "Hello, World!"
    write(*, *) reverse("Hello World!")
end program
