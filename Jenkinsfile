pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                bat 'dotnet restore'
                bat 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                bat 'dotnet test'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                bat 'dotnet format --verify-no-changes'
            }
        }
    }

    post {
        success {
            echo 'Build, Test and Code Quality stages completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}