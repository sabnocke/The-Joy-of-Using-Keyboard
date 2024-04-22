module sieve
    implicit none

    public :: primes
    logical, parameter :: False = .false.
    logical, parameter :: True = .true.
    contains

    function primes(n) result(array)
      integer, intent(in) :: n
      integer :: i, j, counter, subs
      logical, dimension(:), allocatable :: prime
      integer, dimension(:), allocatable :: array

      allocate(prime(n))

      prime = True
      prime(1) = False
      subs = 1

      do i = 2, n
        if (.not. prime(i)) then
          continue
        end if
        do j = i * i, n + 1, i
          prime(j) = False
        end do
      end do

      counter = count(prime .eqv. True)
      allocate(array(counter))

      do i = 1, n
        if (subs .gt. counter) then
          exit
        end if

        if (prime(i)) then
          array(subs) = i
          subs = subs + 1
        end if

      end do
      deallocate(prime)

    end function primes

  end module sieve

  program main
  use sieve

  integer, dimension(:), allocatable :: array

  array = primes(10)
  print *, "---FUNCTION OUTPUT---"
  do i = 1, size(array)
    print *, array(i)
  end do
  print *, array .eq. [2,3,5,7]

  deallocate(array)

  end program main