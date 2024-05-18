(defpackage :socks-and-sexprs
  (:use :cl)
  (:export :lennys-favorite-food )
  )

(in-package :socks-and-sexprs)


#|
fiel:
- Author: ReWyn
- Date: 2024-03-01
|#
(defun lennys-favorite-food ()
  'lasagna
  )

(print lennys-favorite-food)