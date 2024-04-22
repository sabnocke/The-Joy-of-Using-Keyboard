#!/usr/bin/env bash


limit="$1"

if [[ $limit -lt 2 ]]; then
    echo ""
    exit
fi

primes=()
numbers=()


for i in $(seq 1 "$limit");
do
    primes[i]=true
done

primes[0]=false
primes[1]=false

for ((i=2; i<limit+1; i++)); do
    if [[ "${primes[$i]}" == true ]]; then
        for ((j=i*i; j<limit+1; j+=i)); do
            primes[j]=false
        done
    fi
done

for ((i=2; i<limit+1; i++)); do
    if [[ "${primes[$i]}" == true ]]; then
        numbers+=("$i")
    fi
done

echo "${numbers[@]}"