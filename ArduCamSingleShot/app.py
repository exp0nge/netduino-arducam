from flask import Flask, request


app = Flask(__name__)

@app.route('/upload/', methods=['POST'])
def upload():
    with open('capture.jpg', 'wb') as f:
        f.write(request.data)
    return 'OK, saved file'


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
