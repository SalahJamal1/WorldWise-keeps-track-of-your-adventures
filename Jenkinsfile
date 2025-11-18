pipeline {
    agent any

    environment {
        DOTNET_ROOT = tool 'dotnet-8'
        SCANNER_HOME = tool 'sonar-scanner'
    }

    stages {

        stage('Checkout Code') {
            steps {
                git branch: 'main', credentialsId: '466dcc78-0b3c-4b51-9c0b-28a9d809353c', url: 'https://github.com/SalahJamal1/WorldWise-keeps-track-of-your-adventures'
                
            }
        }

        stage('Restore & Build Backend') {
            steps {
                dir('server') {
                    sh '''
                        $DOTNET_ROOT/dotnet restore
                        $DOTNET_ROOT/dotnet build --configuration Release
                    '''
                }
            }
        }

        stage('Run Backend Tests') {
            steps {
                dir('server') {
                    sh '$DOTNET_ROOT/dotnet test --no-build --verbosity normal'
                }
            }
        }

        stage('Build Frontend') {
            steps {
                dir('client') {
                   script {
                        nodejs('node') {
                            sh 'npm install'
                            sh 'npm run build'
                        }
                    }
                }
            }
        }

        stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('sonar-server') {
                    sh '''
                        $SCANNER_HOME/bin/sonar-scanner \
                        -Dsonar.projectKey=mappy \
                        -Dsonar.projectName=mappy \
                        -Dsonar.sources=server
                    '''
                }
            }
        }

        stage('Docker Build & Push') {
            steps {
                script {
                    withCredentials([
                        usernamePassword(credentialsId: '8a0ea94d-bd48-4c46-a69f-6fc0a11e67d1', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')
                    ]) {
                        sh '''
                            echo $DOCKER_PASS | docker login -u $DOCKER_USER --password-stdin
                            docker build --no-cache -t $DOCKER_USER/mappy-backend:latest ./server
                            docker build --no-cache -t $DOCKER_USER/mappy-frontend:latest ./client
                            docker push $DOCKER_USER/mappy-backend:latest
                            docker push $DOCKER_USER/mappy-frontend:latest
                        '''
                    }
                }
            }
        }

        stage('Deploy to Kubernetes') {
            steps {
                dir('k8s') {
                    sh 'kubectl apply -f .'
                }
            }
        }
    }
}
