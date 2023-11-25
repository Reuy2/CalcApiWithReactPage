class Form extends React.Component {

    state = {
        result: undefined,
        error: undefined
    }

    gettingCalc = async (e) => {
        e.preventDefault();
        const operand1 = e.target.elements.firstOperand.value;
        const operand2 = e.target.elements.secondOperand.value;
        const customExp = e.target.elements.customExp.value;
        const typeExp = e.target.elements.operation.value;
        var api_url = undefined;

        if (typeExp == "CustomExp") {

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ expression: customExp })
            };

            api_url = await fetch(`/api/Calc/${typeExp}`, requestOptions);
        }
        else {
            api_url = await fetch(`/api/Calc/${typeExp}?Operand1=${operand1}&Operand2=${operand2}`);
        }
        const data = await api_url.json();

        this.setState({
            result : data.result
        });
    }

    render() {

        const formStyle = {
            border: "2px solid black",
            width: "20%",
            margin: "auto",
            textAlign: "center",

        }
        return <form onSubmit={this.gettingCalc} style={formStyle }>
            <label>Выберите действие:
                <br />
                <select name="operation" >
                    <option value="Plus">Плюс</option>
                    <option value="Minus">Минус</option>
                    <option value="Multiply">Умножить</option>
                    <option value="Divide">Делить</option>
                    <option value="Pow">Степень</option>
                    <option value="Root">Корень</option>
                    <option value="CustomExp">Комплексное Выражение</option>
                </select>
                <br></br>
            </label>

            <label>Первый операнд:
                <br />
                <input type="number" name="firstOperand"></input>
                <br></br>
            </label>

            <label>Второй операнд:
                <br />
                <input type="number" name="secondOperand"></input>
                <br></br>
            </label>

            <label>Поле для кастомного выражения:
                <br />
                <input type="text" name="customExp" />
                <br/>
            </label>

            <label>Результат:
                <br />
                <input type="text" name="result" value={this.state.result } />
                <br/>
            </label>

            <input type="submit" name="sendForm"></input>
        </form>
    }
}
ReactDOM.render(
    <Form />,
    document.getElementById("content")
);