$ ->
    createParentDiv = () ->
        parent = document.createElement('div')
        parent.className = 'row'
        return parent

    createChildDiv = () ->
        childDiv = document.createElement('div')
        childDiv.className = 'col-md-4'
        return childDiv
    
    createInput = (name) ->
        input = document.createElement('input')
        input.type = 'text'
        input.name = name
        return input

    createButton = (name,text) ->   
        button = document.createElement('button')
        button.name = name
        button.innerText = text           
        return button         

    $jqXHRData = null

    createFormulaElement = (formula) ->
        parent = createParentDiv()
        childDivForImage = createChildDiv()
        childDivForImage.className = "col-md-4"
        childDivForButton = createChildDiv()    
        childDivForImage.innerHTML = formula
        childDivForImage.setAttribute('name','AddEquation')
        childButton = createButton("delete","Delete")
        childDivForButton.appendChild childButton
        parent.appendChild childDivForImage
        parent.appendChild childDivForButton        
        return parent   

    initFileUpload = () ->
      $('#imageupload').fileupload({
        url: '/Exercise/UploadImage',
        dataType: 'json',
        add: (e, data) -> 
            $jqXHRData = data
        done: (event, data) -> 
            alert "file uploaded"
            input = $("[name='Text']")[0]
            img1 = "![]( "
            img2 = data.result.path 
            img3 = " \"\")"
            img = img1 + img2 + img3
            input.value = input.value + img
        fail: (event, data) ->
            alert "ERROR"
            if data.files[0].error
              alert data.files[0].error
      })

    $equations = []

    $graphs = []

    $videos = []
    
    getVideos = ()->
        elements = $('iframe')
        $.each elements, (e,value) ->
            $videos.push(value.src)
        return $videos

    getFormulas = () ->
      elements = $("[name='AddEquation']")
      $.each elements, (e,value) ->
        img = value.children[0]
        $equations.push(img.src)
      return $equations

    getGraphs = ()->
        elements = $("[name='GraphInfo']")
        $.each elements, (e,graph) ->
            $graphs.push(graph.value)
        return $graphs


    $(document).ready () ->
        $(document).on 'click', '#addFormula', (e)->
            e.preventDefault()
            img = $('#equationToImg')[0]
            list = $("#listOfFormulas")[0]
            if img.value
                child = createFormulaElement(img.value)
                $('#equationToImg')[0].value = ""
                $('#equation')[0].src = ""
                $('#equationInput')[0].value = ""
                list.appendChild(child)
            return null
        $(document).on 'click', "[name='delete']", (e) ->
            e.preventDefault()
            $(this).unbind()
            parent1 = $(this).parent()
            parent2 = parent1.parent()
            parent2.remove()
            return null
        initFileUpload()
        $(document).on 'click', '#Upload', (e) ->
            e.preventDefault()
            if $jqXHRData
                alert "upload"
                $jqXHRData.submit()
            return false
        $("#addVideo").on 'click', (e) ->
            e.preventDefault()
            parent = createParentDiv()
            parent.innerHTML =  $("#video").val()
            $("#listOfVideos").append parent
            $("#video").val("")
        $("#Submit").on 'click', (e) ->
            e.preventDefault()
            answers = $("#inputAnswers").textext()[0].hiddenInput().val()
            tags = $("#inputTags").textext()[0].hiddenInput().val()
            equation = getFormulas()
            equation = JSON.stringify(equation)
            videos = getVideos()
            videos = JSON.stringify(videos)
            graphs = getGraphs()
            graphs = JSON.stringify(graphs)
            $('input#Graphs').val(graphs)            
            $('input#Category').val($("select#Category").val())    
            $('input#Answers').val(answers)
            $('input#Formulas').val(equation)
            $('input#Tags').val(tags)
            $('input#Videos').val(videos)
        return null