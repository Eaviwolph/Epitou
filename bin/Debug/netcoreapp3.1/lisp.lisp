(defun make-human (name size &rest keys &key birth-year)
(apply #'make-instance 'human :name name :size size
keys))
(qsdfqsdf)