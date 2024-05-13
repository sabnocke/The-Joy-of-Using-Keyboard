module reverse_string
    implicit none
  contains

    function reverse(input) result(reversed)
      character(*), intent(in) :: input
      character(len=len(input)) :: reversed
      integer :: i

      reversed = ""
      do i = len(input), 1, -1
        ! write(*,fmt="(A)", advance='no') input(i:i)
        if (input(i:i) .ne. ' ') then
            reversed = trim(reversed)//input(i:i)
        else
            reversed = ' '//trim(reversed)
        end if
      end do
      reversed = reversed
    end function
  end module

program main

    use reverse_string
    print *, "Hello World"
    write (*, *) reverse("I'm hungry!")
end program