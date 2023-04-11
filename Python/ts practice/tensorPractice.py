from typing import List, Tuple, Dict
import numpy as np
# codiga-disable
import pandas as pd
# codiga-disable
import tensorflow as tf
from pandas import DataFrame, Series
from tensorflow import keras

true: bool = True
false: bool = False
np.set_printoptions(precision=3, suppress=true)


def numerical_normalization(dataframe: DataFrame, inputs: Dict):
    """
    To build the preprocessing model, start by building a set of symbolic tf.keras.Input objects, matching the names
    and data-types of the CSV columns. The first step in your preprocessing logic is to concatenate the numeric
    inputs together, and run them through a normalization layer
    :param inputs: values to input in here (assuming there are some)
    :param dataframe: dataframe from which numerical inputs should be concatenated
    :return: returns normalized dictionary
    """

    for name, column in dataframe.items():
        dtype = column.dtype
        dtype = tf.string if dtype == object else tf.float32
        inputs[name] = keras.Input(shape=(1,), name=name, dtype=dtype)

    numerical = {name: _input for name, _input in inputs.items() if _input.dtype == tf.float32}
    x = keras.layers.Concatenate()(list(numerical.values()))
    norm = keras.layers.Normalization()
    norm.adapt(np.array(dataframe[numerical.keys()]))
    return norm(x)


def string_processing(dataframe: DataFrame, inputs: Dict) -> List:
    """
    For the string inputs use the tf.keras.layers.StringLookup function to map from strings to integer indices in a
    vocabulary. Next, use tf.keras.layers.CategoryEncoding to convert the indexes into float32 data appropriate for
    the model.

    The default settings for the tf.keras.layers.CategoryEncoding layer create a one-hot vector for each input. A
    tf.keras.layers.Embedding would also work. Check out the Working with preprocessing layers guide and the Classify
    structured data using Keras preprocessing layers tutorial for more on this topic.
    :param dataframe: dataframe from which numerical inputs should be processed
    :param inputs:
    :return:
    """
    proc = []
    for name, _input in inputs.items():
        if _input.dtype == tf.float32:
            continue
        lookup = keras.layers.StringLookup(vocabulary=np.unique(dataframe[name]))
        one_hot = keras.layers.CategoryEncoding(num_tokens=lookup.vocabulary_size())

        x = lookup(_input)
        x = one_hot(x)
        proc.append(x)
    return proc


def df_to_dataset(dataframe, shuffle=True, batch_size=32):
    dataframe.copy()
    df = {key: value[:, tf.newaxis] for key, value in dataframe.items()}
    ds = tf.data.Dataset.from_tensor_slices((dict(df)))
    if shuffle:
        ds = ds.shuffle(buffer_size=len(dataframe))
    ds = ds.batch(batch_size)
    ds = ds.prefetch(batch_size)
    return ds


def merge_dict(to_merge: List) -> Dict | bool:
    if isinstance(to_merge, list):
        colossus = {}
        for item in to_merge:
            if isinstance(item, dict):
                colossus |= item
            else:
                continue
        return colossus
    return False


def loading_stuff() -> Tuple[DataFrame, DataFrame, Series, Series]:
    dftrain = pd.read_csv('https://storage.googleapis.com/tf-datasets/titanic/train.csv')  # training data
    dfeval = pd.read_csv('https://storage.googleapis.com/tf-datasets/titanic/eval.csv')  # testing data
    train_label = dftrain.pop('survived')
    eval_label = dfeval.pop('survived')
    return dftrain, dfeval, train_label, eval_label


def titanic_model(preprocessing_head, inputs):
    body = keras.models.Sequential(layers=[
        keras.layers.Dense(64, ),
        keras.layers.Dense(1, ),
    ])

    preprocessed_inputs = preprocessing_head(inputs)
    result = body(preprocessed_inputs)
    model = keras.Model(inputs, result)

    model.compile(
        loss=keras.losses.BinaryCrossentropy(from_logits=true),
        optimizer=keras.optimizers.Adam(),
    )
    return model


def main():
    dftrain, dfeval, train_label, eval_label = loading_stuff()
    inputs = {}
    all_numeric_inputs = numerical_normalization(dftrain, inputs)
    all_string_inputs = string_processing(dftrain, inputs)

    # Collect all the symbolic preprocessing results, to concatenate them later
    preprocessed_inputs = [all_numeric_inputs]

    preprocessed_inputs.extend(all_string_inputs)

    # With the collection of inputs and preprocessed_inputs, you can concatenate all the preprocessed inputs together,
    # and build a model that handles the preprocessing:
    preprocessed_inputs_cat = keras.layers.Concatenate()(preprocessed_inputs)

    titanic_preprocessing = keras.Model(inputs, preprocessed_inputs_cat)

    # keras.utils.plot_model(
    #     model=titanic_preprocessing,
    #     to_file="model.png",
    #     rankdir="LR",
    #     dpi=72,
    #     show_shapes=True
    # )

    # Conversion to dict of tensors (?)
    titanic_feature_dict = {name: np.array(value)
                            for name, value in dftrain.items()}

    # _titanic_model = titanic_model(titanic_preprocessing, inputs)
    # _titanic_model.fit(x=titanic_feature_dict, y=train_label, epochs=10)
    # _titanic_model.save("titanic_model.keras")
    # | Already saved once (use keras.saving.load_model() |


if __name__ == "__main__":
    main()
    print("\nEnd of line")
