module sieve
    implicit none

    public :: prime

    contains

      integer function prime(n)
        integer, intent(in) :: n
        integer :: i, j, count
        logical, dimension(:), allocatable :: primes
        integer, dimension(:), allocatable :: prime_numbers

        allocate(primes(0:n))
        allocate(prime_numbers(0:n))

        primes = .true.
        primes(1) = .false.
        primes(2) = .false.

        do i = 4, n, 2
            primes(i) = .false.
        end do

        do i = 3, n
            if ( primes(i) ) then
                do j = i * i, n, i * 2
                    primes(j) = .false.
                end do
            end if
        end do

        count = 0
        do i = 2, n
            if ( primes(i) ) then
                count = count + 1
                prime_numbers(count) = i
            end if
        end do


        prime = prime_numbers(n)
        do i = 0, 3
            print *, prime_numbers(i)
        end do

        deallocate(primes)
        deallocate(prime_numbers)

      end function prime
    end module sieve

  program main
    use sieve
    print *, prime(10)
    print *, sqrt(4)
  end program main
