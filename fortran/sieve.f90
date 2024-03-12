module sieve
  implicit none

  public :: prime, populate
  ! private :: populate

  contains
    function populate(n) result(arr)
      implicit none
      integer, intent(in) :: n
      integer, dimension(n) :: arr
      integer :: i
      do i = 1, n
        arr(i) = 1
      end do
    end function populate

    integer function prime(n)
      integer, intent(in) :: n
      logical, dimension(n) :: primes
      integer, dimension(n) :: prime_numbers
      ! primes = [(i > 0, i = 1:n)]
    end function prime
  end module sieve

program main
  use sieve
  print *, populate(10)
end program main
